using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager current;
    public AudioSource sound;
    public AudioSource Chucklesound;
    public AudioClip ghostSound;
    public AudioClip breakingSound;
    public AudioClip catMeow;
    public AudioClip ghostChuckle;
    public AudioClip bell;

    private void Awake()
    {
        current = this;
    }

    public static void PlayGhostSound()
    {
        current.sound.PlayOneShot(current.ghostSound);
    }

    public static void PlayBreakingSound()
    {
        current.sound.PlayOneShot(current.breakingSound);
        current.Chucklesound.PlayDelayed(1f);
    }

    public static void PlayMeow()
    {
        current.sound.PlayOneShot(current.catMeow);
    }

    public static void PlayChuckle()
    {
        current.Chucklesound.PlayDelayed(2f);
    }

    public static void PlayBell()
    {
        current.sound.PlayOneShot(current.bell);
    }
}
