using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start() { currentHealth = maxHealth; }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Time.timeScale = 1f; // تأكيد
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        SceneManager.LoadScene("GameOver");
    }
}
