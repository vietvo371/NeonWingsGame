using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("GameScene"); // tên scene gameplay
    }

    public void OnQuitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OnSettingsButton()
    {
        // TODO: mở panel settings (âm thanh, điều khiển...)
    }

    public void OnBackButton()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "GameScene")
        {
            // Tìm PauseMenu trong GameScene và toggle pause
            PauseMenu pause = Object.FindFirstObjectByType<PauseMenu>();
            if (pause != null)
            {
                pause.TogglePause();
            }
            else
            {
                Debug.LogWarning("PauseMenu not found in GameScene");
            }
        }
        else if (currentScene == "Setting" || currentScene == "Settings")
        {
            // Nếu đang ở scene Setting(s) thì quay lại MainMenu
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            // Các scene khác: tuỳ chọn, ở đây tạm không làm gì
            Debug.Log("Back button pressed in scene: " + currentScene);
        }
    }
}
