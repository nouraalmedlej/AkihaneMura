using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    public string mainSceneName = "Akihane_MainScene";
    void Start() { Time.timeScale = 1f; Cursor.lockState = CursorLockMode.None; Cursor.visible = true; }

    public void OnPlayAgain() { SceneManager.LoadScene(mainSceneName); }
    public void OnQuit() { Application.Quit(); }
}

