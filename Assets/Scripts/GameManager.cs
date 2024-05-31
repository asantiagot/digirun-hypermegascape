using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pauseMenuUI;
    public AudioSource audioSource;
    private Vector3 lastCheckpoint;
    private bool isPaused = false;

    void Start()
    {
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        lastCheckpoint = GameObject.FindGameObjectWithTag("Player").transform.position;
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

    // Checkpoint
    public void RestartFromCheckpoint()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
                player.transform.position = lastCheckpoint;
                controller.enabled = true;
            }
            else
            {
                player.transform.position = lastCheckpoint;
            }
        }
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
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
