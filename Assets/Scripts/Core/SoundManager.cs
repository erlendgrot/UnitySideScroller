
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        //instance = this;
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        // Keep this object when we go to a new scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (instance != null && instance != this) {
            Destroy(gameObject);
        }

        // Assign initial volumes for sound and music
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
        
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, soundSource);
    }

    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource);
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        if (currentVolume > 1)
        {
            currentVolume = 0;
        } else if (currentVolume < 0) {
            currentVolume = 1;
        }

        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        // Save final value to player prefs
        PlayerPrefs.SetFloat(volumeName, currentVolume);

    }


}
