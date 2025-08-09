
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CursedShadeAI : MonoBehaviour
{
    [Header("Refs")]
    public Transform player;
    public PlayerHealth playerHealth;        //  PlayerHealth تبع اللاعب هنا
    public AudioSource attackAudio;          //  صوت هجوم
    public Animator anim;                   

    [Header("Movement")]
    public float speed = 3.5f;
    public float rotateSpeed = 6f;
    public float stopDistance = 1.8f;        // مسافة ما قبل الالتصاق

    [Header("Attack")]
    public int damage = 10;                  // int عشان يركب مع PlayerHealth
    public float attackCooldown = 1.2f;      // ثانية بين كل ضربة
    float cd = 0f;

    void Awake()
    {
        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;       // مهم: نخليه Trigger

        
    }

    void Update()
    {
        if (!player || !playerHealth) return;

        cd -= Time.deltaTime;

        // تحريك نحو اللاعب مع دوران سلس
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;
        float dist = toPlayer.magnitude;

        if (dist > stopDistance)
        {
            // لف باتجاه اللاعب
            if (toPlayer.sqrMagnitude > 0.0001f)
            {
                Quaternion target = Quaternion.LookRotation(toPlayer.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, rotateSpeed * Time.deltaTime);
            }

            // تقدّم للأمام
            transform.position += transform.forward * speed * Time.deltaTime;
            if (anim) anim.SetFloat("Speed", 1f);
        }
        else
        {
            if (anim) anim.SetFloat("Speed", 0f);
            TryAttack(); // نحاول نهجم إذا قريب
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // ضربة الدخول كإحساس أولي
        if (other.CompareTag("Player"))
        {
            TryAttack();
        }
    }

    void OnTriggerStay(Collider other)
    {
        // استمرار الهجوم مع كول داون
        if (other.CompareTag("Player"))
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (cd > 0f || playerHealth == null) return;

        cd = attackCooldown;

        playerHealth.TakeDamage(damage);

        if (attackAudio) attackAudio.Play();
        if (anim) anim.SetTrigger("Attack");

        // هز الكاميرا 
        if (CameraShake.Instance) CameraShake.Instance.Shake(0.2f, 0.2f);

        // AudioManager 
        if (AudioManager.Instance)
        {
            AudioManager.Instance.PlayHurt();        // صوت إصابة اللاعب
            AudioManager.Instance.PlayEnemySound();   // صوت الوحش (اختياري)
        }
    }
}

