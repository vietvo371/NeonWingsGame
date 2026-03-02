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
}
