using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusesView : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SkinsBonuses skinsBonuses;

    [SerializeField] private GameObject shield;
    [SerializeField] private TMP_Text shieldTimerText;
    private float shieldTimer;

    [SerializeField] private GameObject doubleCarrot;
    [SerializeField] private TMP_Text doubleCarrotTimerText;
    private float doubleCarrotTimer;

    [SerializeField] private GameObject jetpack;
    [SerializeField] private TMP_Text jetpackTimerText;
    private float jetpackTimer;

    private bool isShieldRunning = false;
    private bool isDoubleCarrotRunning = false;
    private bool isJetpackRunning = false;


    private void Start()
    {
        shield.SetActive(false);
        doubleCarrot.SetActive(false);
        jetpack.SetActive(false);

    }

    private void Update()
    {
        if (isShieldRunning == true)
        {
            shieldTimer -= Time.deltaTime;
            shieldTimerText.text = Mathf.RoundToInt(shieldTimer).ToString();
            if (shieldTimer <= 0)
            {
                shield.SetActive(false);
            }
        }

        if (isDoubleCarrotRunning == true)
        {
            doubleCarrotTimer -= Time.deltaTime;
            doubleCarrotTimerText.text = Mathf.RoundToInt(doubleCarrotTimer).ToString();
            if (doubleCarrotTimer <= 0)
            {
                doubleCarrot.SetActive(false);
            }
        }

        if (isJetpackRunning == true)
        {
            jetpackTimer -= Time.deltaTime;
            jetpackTimerText.text = Mathf.RoundToInt(jetpackTimer).ToString();
            if (jetpackTimer <= 0)
            {
                jetpack.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        player.ShieldActivated += OnShieldActivated;
        player.DoubleCarrotActivated += OnDoubleCarrotActivated;
        player.JetpackActivated += OnJetpackActivated;
    }

    private void OnDisable()
    {
        player.ShieldActivated -= OnShieldActivated;
        player.DoubleCarrotActivated -= OnDoubleCarrotActivated;
        player.JetpackActivated -= OnJetpackActivated;
    }

    private void OnShieldActivated()
    {

        shieldTimer = player.StandartBonusDuration * skinsBonuses.ShieldSkinBoost;
        shield.SetActive(true);
        isShieldRunning = true;
    }

    private void OnDoubleCarrotActivated()
    {

        doubleCarrotTimer = player.StandartBonusDuration * skinsBonuses.DoubleCarrotSkinBoost;
        doubleCarrot.SetActive(true);
        isDoubleCarrotRunning = true;
    }
    private void OnJetpackActivated()
    {

        jetpackTimer = player.StandartBonusDuration * skinsBonuses.JetpackSkinBoost;
        jetpack.SetActive(true);
        isJetpackRunning = true;
    }
}
