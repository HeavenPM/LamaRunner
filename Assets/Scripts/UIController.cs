using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreViewer;
    [SerializeField] private TMP_Text GO_ScoreViewer;
    [SerializeField] private GameObject pauseMenu;

    public Player Player;
    public AudioSource AudioSource;

    private float score;
    private int highscore;
    private int totalCarrots;

    private void Start()
    {
        pauseMenu.SetActive(false);
        AudioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        score += Time.fixedDeltaTime * 10;

        scoreViewer.text = "Score: " + ((int)score).ToString();
        GO_ScoreViewer.text = "Score: " + ((int)score).ToString();

        highscore = PlayerPrefs.GetInt("Highscore", highscore);
        if (score > highscore)
        {
            highscore = (int)score;
            PlayerPrefs.SetInt("Highscore", highscore);
        }
    }

    public void PauseGame()
    {
        AudioSource.Play();
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        AudioSource.Play();
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void RestartGame()
    {
        AudioSource.Play();
        Time.timeScale = 1;
        Invoke("LoadRestartedLevel", 0.5f);
    }

    public void GoToMenuFromGameOver()
    {
        AudioSource.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void GoToMenuFromPause()
    {
        AudioSource.Play();
        totalCarrots = PlayerPrefs.GetInt("TotalCarrots", totalCarrots);
        totalCarrots += Player.CarrotsCount;
        PlayerPrefs.SetInt("TotalCarrots", totalCarrots);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void LoadRestartedLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
