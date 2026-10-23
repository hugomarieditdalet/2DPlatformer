using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource AudioSource;
    private int musicIndex = 0;
    public AudioMixerGroup soundEffectMixer;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de AudioManager dans la scène ");
            return;
        }
        instance = this;
    }

    void Start()
    {
        AudioSource.clip = playlist[0];
        AudioSource.Play();
    }

    private void Update()
    {
        if (!AudioSource.isPlaying)
        {
            PlayNextSong();
        }
    }
    void PlayNextSong()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        AudioSource.clip = playlist[musicIndex];
        AudioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clib , Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clib;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        Destroy(tempGO, clib.length);
        return audioSource;
    }
}
