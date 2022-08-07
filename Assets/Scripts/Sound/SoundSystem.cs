using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public static SoundSystem instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        //keep the object even after going to a new level
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //destroy duplicate objects
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }

    public void Play(AudioClip _sound)
    {
        audioSource.PlayOneShot(_sound);
    }
}
