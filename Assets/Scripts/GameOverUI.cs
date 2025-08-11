using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public string mainSceneName = "Akihane_MainScene";
    void Start() { Time.timeScale = 1f; Cursor.lockState = CursorLockMode.None; Cursor.visible = true; }

    public void OnRetry() { SceneManager.LoadScene(mainSceneName); }
    public void OnQuit() { Application.Quit(); }
}

