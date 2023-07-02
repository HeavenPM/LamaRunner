using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]


public class Player : MonoBehaviour
{
    public UnityAction ShieldActivated;
    public UnityAction DoubleCarrotActivated;
    public UnityAction JetpackActivated;

    public GameObject JumpButton;
    public int SkinIndex;
    public float StandartBonusDuration;
    public int CarrotsCount;

    public AudioSource JumpSound;
    public AudioSource BonusSound;
    public AudioSource CarrotCollectedSound;
    public AudioClip JumpClip;
    public AudioClip BonusClip;
    public AudioClip CarrotCollectedClip;
    


    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJumpForce;
    [SerializeField] private SkinsBonuses skinsBonuses;
    
    [SerializeField] private Transform parent;
    [SerializeField] private TMP_Text carrotsViewer;
    [SerializeField] private GameObject shieldEffect;
    [SerializeField] private GameObject jetpackEffect;
    [SerializeField] private ParticleSystem damageTakenEffect;
    [SerializeField] private ParticleSystem landingEffect;
    [SerializeField] private Transform landingEffectPos;
    [SerializeField] private Carrot carrotTemplate;

    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TMP_Text gOM_TotalCarrots;

    private Vector3 moveDirection;
    private Vector3 jumpDirection;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private bool isGrounded;
    private bool isImmortal;
    private bool isFlying = false;
    
    private bool isBonusShieldActive = false;
    private bool isBonusDoubleCarrotActive = false;
    private bool isJetpackActive = false;

    private int totalCarrotsCount;
    private int carrotsBonusFactor;

    private Coroutine SpeedIncreaseCoroutine;
    private Coroutine ShieldBonusCoroutine;
    private Coroutine DoubleCarrotCoroutine;
    private Coroutine JetpackCoroutine;

    private void Awake()
    {
        SkinIndex = PlayerPrefs.GetInt("chosenSkin", SkinIndex);
        
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        
        Button jumpB = JumpButton.GetComponent<Button>();
        jumpB.onClick.AddListener(PlayerJump);

        moveDirection = Vector3.forward;
        jumpDirection = Vector3.up;

        SpeedIncreaseCoroutine = StartCoroutine(SpeedIncrease());

        isImmortal = false;
        carrotsBonusFactor = skinsBonuses.CarrotFactor;
        gameOverMenu.SetActive(false);

        JumpSound = GetComponent<AudioSource>();
        BonusSound = GetComponent<AudioSource>();
        CarrotCollectedSound = GetComponent<AudioSource>();


    }

    private void FixedUpdate()
    {
        transform.Translate(moveDirection * playerSpeed * Time.fixedDeltaTime);

        if (isGrounded == false)
        {
            _animator.SetBool("isRun", false);
        }
        else
        {
            _animator.SetBool("isRun", true);
        }

        if (isBonusShieldActive == false)
        {
            shieldEffect.SetActive(false);
        }
        if (isJetpackActive == false)
        {
            jetpackEffect.SetActive(false);
        }

        if (isFlying)
        {
            isImmortal = true;
            transform.Translate(Vector3.up * playerJumpForce * Time.fixedDeltaTime);

            if (transform.position.y > -5.75f)
            {
                transform.position = new Vector3(transform.position.x, -5.75f, transform.position.z);
                _animator.SetTrigger("isFly");
                _animator.SetBool("isRun", false);
            }
        }

        if (isBonusShieldActive)
        {
            isImmortal = true;
        }

        
        playerJumpForce = skinsBonuses.JumpForceBoost;

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Obstacle obstacle) && isImmortal == false && skinsBonuses.IsExtraLive == false)
        {
            playerSpeed = 0;
            _rigidbody.AddForce(Vector3.back * 10, ForceMode.Impulse);
            ParticleSystemRenderer renderer = Instantiate(damageTakenEffect, landingEffectPos.position, Quaternion.identity).GetComponent<ParticleSystemRenderer>();
            
            StartCoroutine(GameOver());
        }
        else if (other.gameObject.TryGetComponent(out obstacle) && isImmortal == false && skinsBonuses.IsExtraLive == true)
        {
            ParticleSystemRenderer renderer = Instantiate(damageTakenEffect, landingEffectPos.position, Quaternion.identity).GetComponent<ParticleSystemRenderer>();
            skinsBonuses.IsExtraLive = false;
            JetpackActivated?.Invoke();
            
            JetpackCoroutine = StartCoroutine(BonusJetpackActivated());
        }

            if (other.gameObject.TryGetComponent(out Water water) && isImmortal == true)
        {
            _rigidbody.AddForce(jumpDirection * playerJumpForce, ForceMode.Impulse);
        }

        if (other.gameObject.TryGetComponent(out Carrot carrot))
        {
            CarrotsCount += carrotsBonusFactor;
            carrotsViewer.text = CarrotsCount.ToString();
            CarrotCollectedSound.PlayOneShot(CarrotCollectedClip);
            
        }


        if (other.gameObject.TryGetComponent(out BonusShield bonusShield))
        {
            BonusSound.PlayOneShot(BonusClip);
            ShieldActivated?.Invoke();

            if (isBonusShieldActive == true)
            {
                StopCoroutine(ShieldBonusCoroutine);
                isBonusShieldActive = false;
                shieldEffect.SetActive(false);
                ShieldBonusCoroutine = StartCoroutine(BonusShieldActivated());
            }
            else
                ShieldBonusCoroutine = StartCoroutine(BonusShieldActivated());
        }

        if (other.gameObject.TryGetComponent(out BonusDoubleCarrot bonusDoubleCarrot))
        {
            BonusSound.PlayOneShot(BonusClip);
            DoubleCarrotActivated?.Invoke();

            if (isBonusDoubleCarrotActive == true)
            {
                StopCoroutine(DoubleCarrotCoroutine);
                isBonusDoubleCarrotActive = false;
                carrotsBonusFactor = skinsBonuses.CarrotFactor;
                DoubleCarrotCoroutine = StartCoroutine(BonusDoubleCarrotActivated());
            }
            else
                DoubleCarrotCoroutine = StartCoroutine(BonusDoubleCarrotActivated());
        }

        if (other.gameObject.TryGetComponent(out Jetpack jetpack))
        {
            BonusSound.PlayOneShot(BonusClip);
            JetpackActivated?.Invoke();

            if (isJetpackActive == true)
            {
                StopCoroutine(JetpackCoroutine);
                isJetpackActive = false;
                jetpackEffect.SetActive(false);
                JetpackCoroutine = StartCoroutine(BonusJetpackActivated());

            }
            else
                JetpackCoroutine = StartCoroutine(BonusJetpackActivated());
        }


        if (other.gameObject.TryGetComponent(out GroundTrigger groundTrigger))
        {
            isGrounded = true;
            _animator.SetTrigger("isRun");
            ParticleSystemRenderer renderer = Instantiate(landingEffect, landingEffectPos.position, Quaternion.identity).GetComponent<ParticleSystemRenderer>();
        }

    }

    

    private void PlayerJump()
    {

        if ( isGrounded)
        {
            JumpSound.PlayOneShot(JumpClip);
            _rigidbody.AddForce(jumpDirection * playerJumpForce, ForceMode.Impulse);
            StartCoroutine(JumpHappen());
        }
        
    }

    IEnumerator JumpHappen()
    {
        isGrounded = false;
        _animator.SetTrigger("isJump");
        yield return new WaitForSeconds(0.7f);

        _rigidbody.AddForce(Vector3.down * 2, ForceMode.Impulse);
    }

    IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(10);
        playerSpeed++;
        if (playerSpeed > 23)
            playerSpeed = 23;
        StartCoroutine(SpeedIncrease());

    }

    IEnumerator BonusShieldActivated()
    {
        
        isBonusShieldActive = true;
        isImmortal = true;
        shieldEffect.SetActive(true);
        yield return new WaitForSeconds(StandartBonusDuration * skinsBonuses.ShieldSkinBoost);
        
        shieldEffect.SetActive(false);
        if (isJetpackActive == false)
        {
            isImmortal = false;
            
        }
        isBonusShieldActive = false;
    }

    IEnumerator BonusDoubleCarrotActivated()
    {

        isBonusDoubleCarrotActive = true;
        carrotsBonusFactor *=2;
        yield return new WaitForSeconds(StandartBonusDuration * skinsBonuses.DoubleCarrotSkinBoost);
        carrotsBonusFactor/=2;
        isBonusDoubleCarrotActive = false;

    }

    IEnumerator BonusJetpackActivated()
    {

        isJetpackActive = true;
        isImmortal = true;
        jetpackEffect.SetActive(true);
        JumpButton.SetActive(false);

        StartCoroutine(JetpackCarrotSpawner());

        isFlying = true;
        isGrounded = false;
        yield return new WaitForSeconds(0.5f);
        _rigidbody.isKinematic = true;
        _animator.SetBool("isRun", false);
        _animator.SetTrigger("isFly");
        yield return new WaitForSeconds(StandartBonusDuration * skinsBonuses.JetpackSkinBoost);
        _rigidbody.isKinematic = false;
        isFlying = false;
        yield return new WaitForSeconds(1);
        _animator.SetBool("isRun", true);
        JumpButton.SetActive(true);
        jetpackEffect.SetActive(false);
        yield return new WaitForSeconds(2);
        JumpButton.SetActive(true);
        isImmortal = false;
        isJetpackActive = false;

    }

    IEnumerator JetpackCarrotSpawner()
    {
        for (int i = 1; i < 21; i++)
        {
            Instantiate(carrotTemplate, new Vector3(transform.position.x - 0.694f, -4.75f, transform.position.z + 2 * i), Quaternion.Euler(0, 90, 0));
        }
        yield return new WaitForSeconds(5);
        if (isJetpackActive)
        {
            StartCoroutine(JetpackCarrotSpawner());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
        totalCarrotsCount = PlayerPrefs.GetInt("TotalCarrots", totalCarrotsCount);
        totalCarrotsCount += CarrotsCount;
        PlayerPrefs.SetInt("TotalCarrots", totalCarrotsCount);
        gOM_TotalCarrots.text = totalCarrotsCount.ToString();
    }
}
