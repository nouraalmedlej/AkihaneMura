using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    Vector3 originalPos;
    float timeLeft;
    float magnitude;

    void Awake()
    {
        Instance = this;
        originalPos = transform.localPosition;
    }

    // بدون بارامترات
    public void Shake() { Shake(0.15f, 0.2f); }

    // شدّة فقط
    public void Shake(float mag) { Shake(0.15f, mag); }

    // المدة + الشدّة
    public void Shake(float duration, float mag)
    {
        timeLeft = duration;
        magnitude = mag;
    }

    void LateUpdate()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.unscaledDeltaTime;
            transform.localPosition = originalPos + Random.insideUnitSphere * magnitude;
            if (timeLeft <= 0f) transform.localPosition = originalPos;
        }
    }
}

