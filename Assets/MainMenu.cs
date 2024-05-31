using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject startMenuCanvas;
    public PlayerController playerController;

    void Start()
    {
        if (startMenuCanvas != null)
        {
            startMenuCanvas.SetActive(true);
        }
    }

    public void StartGame()
    {
        if (playerController != null)
        {
            playerController.StartGame();
            startMenuCanvas.SetActive(false);
        }
    }

    public void SelectLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
