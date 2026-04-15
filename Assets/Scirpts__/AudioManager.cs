using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance
    public AudioSource bgMusicSource;   // Background music AudioSource
    public AudioSource sfxSource;       // Sound effects AudioSource

    [Header("Background Music")]
    public AudioClip bgMusicClip;       // The background music clip

    [Header("Sound Effects")]
    public AudioClip buttonClickClip;   // Button click sound effect clip
    public AudioClip YesClick;   // Button click sound effect clip
    public AudioClip NoClick;   // Button click sound effect clip
    public AudioClip UISwipe;   // Button click sound effect clip
    public AudioClip Complete;   // Button click sound effect clip

    private bool isMusicPlaying = true;

    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Don't destroy this object when loading a new scene
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        // Start playing background music if not already playing
        if (bgMusicSource != null && isMusicPlaying)
        {
            bgMusicSource.clip = bgMusicClip;
            bgMusicSource.loop = true;
            bgMusicSource.Play();
        }
    }

    // Function to toggle background music
    public void MusicOf()
    {
      //  if (bgMusicSource.isPlaying)
        {
            bgMusicSource.Pause();
            isMusicPlaying = false;
        }
   
    }


    public void MusicOn()
    {
        //if (bgMusicSource.isPlaying)
        //{
        //    bgMusicSource.Pause();
        //    isMusicPlaying = false;
        //}
        //else
        {
            bgMusicSource.Play();
            isMusicPlaying = true;
        }
    }


    //   AudioManager.Instance.PlayButtonClickSound();
    //  AudioManager.Instance.ToggleMusic();


    // Play button click sound effect
    public void PlayButtonClickSound()
    {
        if (sfxSource != null && buttonClickClip != null)
        {
            sfxSource.PlayOneShot(buttonClickClip);
        }
    }

    // Play any other sound effects
    public void PlaySoundEffect(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void OtherSound(string name)
    {
        switch (name)
        {
            case "Yes":
                if (sfxSource != null)
                {
                    sfxSource.PlayOneShot(YesClick);
                }
                break;

            case "No":
                if (sfxSource != null)
                {
                    sfxSource.PlayOneShot(NoClick);
                }
                break;


            case "UI":
                if (sfxSource != null)
                {
                    sfxSource.PlayOneShot(UISwipe);
                }
                break;


            case "Complete":
                if (sfxSource != null)
                {
           
                    sfxSource.PlayOneShot(Complete);
                }
                break;

        }
    }



    // You can also add more sound-related functionality here (like volume control, etc.)

    public void SetMusicVolume(float volume)
    {
        bgMusicSource.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
