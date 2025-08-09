using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource audioSource;
    public AudioClip hurtSound;
    public AudioClip enemySound;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayHurt()
    {
        audioSource.PlayOneShot(hurtSound,100);
    }

    public void PlayEnemySound()
    {
        audioSource.PlayOneShot(enemySound,100);
    }
}
