using UnityEngine;

public class SealSoundTrigger : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // نحصل على الـ AudioSource من نفس العنصر
        audioSource = GetComponent<AudioSource>();

        // نتأكد أن الصوت مفعّل
        if (audioSource != null)
        {
            audioSource.enabled = true;
            AudioListener.pause = false;
            AudioListener.volume = 1f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // إذا دخل اللاعب المربع
        if (other.CompareTag("Player"))
        {
            Debug.Log("📜 Collected the stamp!");

            // إذا كان الصوت جاهز
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play(); // نشغل الصوت
            }

            // نخفي الختم بعد جمعه
            gameObject.SetActive(false);
        }
    }
}

