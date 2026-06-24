using UnityEngine;

// This script is managing all game audio and sound Effects
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Clips")]
    public AudioClip leverPullSound; //Lever Pull Sound Audio Clip
    public AudioClip reelSpinSound; //Looping sound while reels are spinning
    public AudioClip reelStopSound; //click sound when each reel stops (plays 3 times -> 3 reels)
    public AudioClip winSound; // normal win sound
    public AudioClip jackpotSound; //Jackpot sound only on 777
    public AudioClip lossSound; //Lose sound audio clip

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float spinLoopVolume = 0.5f;
    private AudioSource sfxSource;
    private AudioSource loopSource;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;

        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.playOnAwake = false;
        loopSource.loop = true;
    }

    // Play lever pull sound
    public void PlayLever()
    {
        PlaySFX(leverPullSound);
    }

    // Start looping reel spin sound
    public void PlaySpinLoop()
    {
        if (reelSpinSound == null) return;
        loopSource.clip = reelSpinSound;
        loopSource.volume = spinLoopVolume;
        loopSource.Play();
    }

    // Stop the reel spin loop
    public void StopSpinLoop()
    {
        loopSource.Stop();
    }

    // Play reel stop click — call once per reel
    public void PlayReelStop()
    {
        PlaySFX(reelStopSound);
    }

    // Play win sound
    public void PlayWin()
    {
        PlaySFX(winSound);
    }

    // Play jackpot sound
    public void PlayJackpot()
    {
        PlaySFX(jackpotSound);
    }

    // Play loss sound
    public void PlayLoss()
    {
        PlaySFX(lossSound);
    }
    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }
}