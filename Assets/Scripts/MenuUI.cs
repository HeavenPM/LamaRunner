using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public AudioSource AudioSource;
    
    [SerializeField] private TMP_Text highscoreText;

    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject shopPanel;

    private int totalCarrots;
    private int highscore;

    private void Start()
    {
        mainPanel.SetActive(true);
        shopPanel.SetActive(false);

        AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
        highscore = PlayerPrefs.GetInt("Highscore", highscore);
        highscoreText.text = "Highscore " + highscore.ToString();
    }

    public void EnterShop()
    {
        mainPanel.SetActive(false);
        shopPanel.SetActive(true);
        AudioSource.Play();
    }

    public void BackToMenu()
    {
        AudioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayGame()
    {
        AudioSource.Play();
        Invoke("LoadGame", 0.5f);
    }

    public void QuitGame()
    {
        AudioSource.Play();
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
