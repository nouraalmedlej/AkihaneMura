using UnityEngine;

public class AudioFix : MonoBehaviour
{
    void Start()
    {
        AudioListener.pause = false;
        AudioListener.volume = 1f;
        Debug.Log("sound on");
    }
}
