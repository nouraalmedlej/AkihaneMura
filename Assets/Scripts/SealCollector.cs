using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SealCollector : MonoBehaviour
{
    [Header("FX")]
    public AudioSource sfx;
    public GameObject vfx;

    [Header("Pickup")]
    public bool requireKey = false;      // لو تبين الالتقاط بزر E
    public KeyCode key = KeyCode.E;
    public string playerTag = "Player";
    public float pickupDelay = 0.25f;    // مهلة قصيرة لمنع الالتقاط وقت بدء المشهد

    bool taken = false;
    float enableTime;
    bool playerInRange = false;

    void Reset()
    {
        // يضمن الـCollider موجود ومضبوط كتريغر
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void OnEnable()
    {
        enableTime = Time.time + pickupDelay;
        taken = false;
        playerInRange = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        playerInRange = true;
        if (!requireKey) TryPickup();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        playerInRange = false;
    }

    void Update()
    {
        if (requireKey && playerInRange && Input.GetKeyDown(key))
            TryPickup();
    }

    void TryPickup()
    {
        if (taken) return;
        if (Time.time < enableTime) return; // لا يلتقط فور البدء
        taken = true;

        if (sfx) sfx.Play();
        if (vfx) vfx.SetActive(true);

        // اخفِ الشكل وأوقف الاصطدام
        foreach (var r in GetComponentsInChildren<Renderer>()) r.enabled = false;
        var col = GetComponent<Collider>(); if (col) col.enabled = false;

        // سجّل الختم
        if (SealManager.I != null) SealManager.I.RegisterSealPickup();

        // تدمير بعد انتهاء الصوت (أو بسرعة لو ما فيه صوت)
        float t = (sfx && sfx.clip) ? sfx.clip.length : 0.05f;
        Destroy(gameObject, t);
    }
}





