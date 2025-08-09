using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject gameOverPanel;

    [Header("UI")]
    public Slider healthBar;
    public TMP_Text hpText;

    void Start()
    {
        currentHealth = maxHealth;
        if (gameOverPanel) gameOverPanel.SetActive(false);
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        UpdateUI();
        if (currentHealth <= 0) Die();
    }

    void UpdateUI()
    {
        if (healthBar) healthBar.value = currentHealth;
        if (hpText) hpText.text = $"HP: {currentHealth}";
    }

    void Die()
    {
        Time.timeScale = 0f;
        if (gameOverPanel) gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}


