using UnityEngine;

public class SimpleUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject startPanel;   // اسحبي StartPanel هنا
    public GameObject pausePanel;   // اختياري

    [Header("Camera Control")]
    public MonoBehaviour cameraScript; // اسحبي سكربت الكاميرا (مثلاً CameraFollowTP)

    bool started = false;
    bool paused = false;

    void Start()
    {
        Time.timeScale = 0f;
        if (startPanel) startPanel.SetActive(true);
        if (pausePanel) pausePanel.SetActive(false);
        if (cameraScript) cameraScript.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnPlay()
    {
        started = true;
        if (startPanel) startPanel.SetActive(false);
        Resume();
    }

    public void OnToggleMute()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (!started) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused) Resume();
            else Pause();
        }
    }

    void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
        if (pausePanel) pausePanel.SetActive(true);
        if (cameraScript) cameraScript.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Resume()
    {
        paused = false;
        Time.timeScale = 1f;
        if (pausePanel) pausePanel.SetActive(false);
        if (cameraScript) cameraScript.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

