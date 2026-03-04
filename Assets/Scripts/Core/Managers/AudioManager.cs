using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource sfxSource;


    void Awake()
    {
        Instance = this;
    }

    public void PlaySound(AudioClip sfx, bool randomPitch = false)
    {
        float pitch = randomPitch ? Random.Range(0.95f, 1.05f) : 1f;
        sfxSource.pitch = pitch;
        sfxSource.PlayOneShot(sfx);
    }
}
