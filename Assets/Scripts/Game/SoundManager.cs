using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("audio")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip dragClip;
    [SerializeField] private AudioClip dropClip;
    [SerializeField] private AudioClip placeClip;
    [SerializeField] private AudioClip clearLineClip;
    [SerializeField] private AudioClip loseClip;

    void Awake()
    {
        if(Instance !=null && Instance!=this)
        {
            Destroy(gameObject);
            return;
        }
        Instance=this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
{
    if (musicSource == null)
    {
        Debug.LogError("Music Source chưa được gán!");
        return;
    }

    if (backgroundMusic == null)
    {
        Debug.LogError("Background Music chưa được gán!");
        return;
    }

    Debug.Log("Đang phát nhạc: " + backgroundMusic.name);

    musicSource.clip = backgroundMusic;
    musicSource.loop = true;
    musicSource.volume = 1f;
    musicSource.Play();

    Debug.Log("Music đang chạy: " + musicSource.isPlaying);
}
    public void PlayDragSound()
    {
        sfxSource.PlayOneShot(dragClip);
    }

    public void PlayDropSound()
    {
        sfxSource.PlayOneShot(dropClip);
    }

    public void PlayPlaceSound()
    {
        sfxSource.PlayOneShot(placeClip);
    }

    public void PlayClearLineSound()
    {
        sfxSource.PlayOneShot(clearLineClip);
    }

    public void PlayLoseSound()
    {
        sfxSource.PlayOneShot(loseClip);
    }

    public void ToggleMusic()
    {
        if(musicSource !=null)
        {
            musicSource.mute = !musicSource.mute;
        }
    }

    public void ToggleSFX()
    {
        if(sfxSource !=null)
        {
            sfxSource.mute=!sfxSource.mute;
        }
    }

    public bool IsMusicMuted => musicSource != null && musicSource.mute;
    public bool IsSFXMuted => sfxSource != null && sfxSource.mute;

}
