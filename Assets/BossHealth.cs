using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int maxHP = 100;
    int hp;
// void OnEnable() { hp = maxHP; } // كل مرة يظهر
// void OnEnable() { hp = maxHP; } // كل مرة يظهر

    public void TakeDamage(int amount)
    {
        hp = Mathf.Max(0, hp - amount);
        if (hp <= 0) OnDeath();
    }

    void OnDeath()
    {
        SceneManager.LoadScene("Victory");
    }
}

