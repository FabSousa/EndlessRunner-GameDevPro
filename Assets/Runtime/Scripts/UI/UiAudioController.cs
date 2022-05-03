using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UiAudioController : MonoBehaviour
{
    [SerializeField] AudioClip buttonSound;
    [SerializeField] AudioClip countdownDound;
    [SerializeField] AudioClip lastCountdownDound;


    AudioSource audioSource;

    AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayButtonSound()
    {
        Play(buttonSound);
    }

    public void PlayCountdownSound()
    {
        Play(countdownDound);
    }

    public void PlayLastCountdownSound()
    {
        Play(lastCountdownDound);
    }

    private void Play(AudioClip clip)
    {
        AudioUtility.PlayAudioCue(AudioSource, clip);
    }
}
