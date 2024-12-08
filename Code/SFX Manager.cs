using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [Header("Movement SFX")]
    public AudioClip runSound;
    public AudioClip wallJumpSound;
    public AudioClip jumpSound;

    [Header("Pedang SFX")]
    public AudioClip swordHitSound;
    public AudioClip swordMissSound;

    [Header("Damage SFX")]
    public AudioClip hitSound;
    public AudioClip damageTakenSound;

    [Header("Collect SFX")]
    public AudioClip collectItemSound;

    private AudioSource mainAudioSource; 
    private AudioSource runAudioSource; 

    private AudioSource[] allAudioSources;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        mainAudioSource = gameObject.AddComponent<AudioSource>();
        runAudioSource = gameObject.AddComponent<AudioSource>();
        runAudioSource.loop = true;
        allAudioSources = FindObjectsOfType<AudioSource>();
    }
    public void PlayRunSound()
    {
        if (runSound != null && !runAudioSource.isPlaying)
        {
            runAudioSource.clip = runSound;
            runAudioSource.Play();
        }
    }
    public void StopRunSound()
    {
        if (runAudioSource.isPlaying)
        {
            runAudioSource.Stop();
        }
    }
    public void PlayWallJumpSound()
    {
        PlaySound(wallJumpSound);
    }
    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }
    public void PlaySwordHitSound()
    {
        PlaySound(swordHitSound);
    }
    public void PlaySwordMissSound()
    {
        PlaySound(swordMissSound);
    }
    public void PlayHitSound()
    {
        PlaySound(hitSound);
    }
    public void PlayDamageTakenSound()
    {
        PlaySound(damageTakenSound);
    }
    public void PlayCollectItemSound()
    {
        PlaySound(collectItemSound);
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            mainAudioSource.PlayOneShot(clip);
        }
    }
    public void PauseAllSFX()
    {
        foreach (var source in allAudioSources)
        {
            if (source != null && source.isPlaying)
            {
                source.Pause();
            }
        }
    }
    public void ResumeAllSFX()
    {
        foreach (var source in allAudioSources)
        {
            if (source != null)
            {
                source.UnPause();
            }
        }
    }
}
