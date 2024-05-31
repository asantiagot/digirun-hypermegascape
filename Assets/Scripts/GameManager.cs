using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pauseMenuUI;
    public AudioSource audioSource;
    private bool isPaused = false;

    void Start()
    {
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (HealthManager.instance.currentHealth <= 0)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Pause/Resume
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;

        if (audioSource != null)
        {
            audioSource.Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;

        if (audioSource != null)
        {
            audioSource.UnPause();
        }
    }

    // Game Over methods
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
