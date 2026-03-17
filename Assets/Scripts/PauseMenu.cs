using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject gameOverPanel; // Tham chiếu đến Game Over Panel
    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // Đảm bảo PausePanel tắt khi bắt đầu
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // Đảm bảo GameOverPanel tắt khi bắt đầu
        }
    }

    void Update()
    {
        // Cho phép bấm ESC để pause/unpause trên PC
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     TogglePause();
        // }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void OnResumeButton()
    {
        if (!isPaused) return;
        TogglePause();
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1f;
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Dừng thời gian
        Time.timeScale = 0f;
    }
}