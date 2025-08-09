using UnityEngine;
using TMPro;

public class SealManager : MonoBehaviour
{
    public static SealManager I;

    [Header("Seals")]
    public int totalSeals = 3;
    public int collected = 0;

    [Header("UI")]
    public TMP_Text sealText;
    public TMP_Text tipText;

    [Header("Boss / Gate")]
    public GameObject boss;
    public GameObject bossGate;
    public string bossName = "Yūrei (幽霊)";

    [Header("SFX")]
    public AudioSource allSealsSfx;

    bool bossSpawned = false;             // <-- يمنع التكرار

    void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; } // <-- لا تتكرر
        I = this;
    }

    void Start()
    {
        if (tipText) tipText.text = "";   // <-- فضّي الرسالة من البداية
        if (boss) boss.SetActive(false);
        UpdateUI();
    }

    public void RegisterSealPickup()
    {
        if (collected >= totalSeals) return;

        collected++;
        UpdateUI();

        if (!bossSpawned && collected >= totalSeals)
        {
            bossSpawned = true;
            if (allSealsSfx) allSealsSfx.Play();
            if (boss) boss.SetActive(true);
            if (bossGate) bossGate.SetActive(false);

            if (tipText)
            {
                tipText.text = $"All seals collected! The {bossName} has appeared.";
                CancelInvoke(nameof(ClearTip));
                Invoke(nameof(ClearTip), 3f);
            }
        }
    }

    void UpdateUI()
    {
        if (sealText) sealText.text = $"SEALS: {collected}/{totalSeals}";
    }

    void ClearTip()
    {
        if (tipText) tipText.text = "";
    }
}
