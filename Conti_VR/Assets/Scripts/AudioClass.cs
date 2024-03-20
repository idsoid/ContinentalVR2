using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClass : MonoBehaviour
{
    //name of the audio
    public string audioName;
    //array of audiosources on the gameobject
    public AudioSource[] audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //get all audio sources
        audioSource = GetComponents<AudioSource>();
    }

    //returns the array of audioSources
    public AudioSource[] GetAudioSource()
    {
        return audioSource;
    }
    //Play all audioSources on this class
    public void PlayAudio()
    {
        for (int j = 0; j < audioSource.Length; ++j)
        {
            AudioSource childAC = audioSource[j];
            if (!childAC.isPlaying)
            {
                StartCoroutine(FadeIn(childAC));
            }
        }
    }
    //Stop all audioSources on this class
    public void StopAudio()
    {
        for (int j = 0; j < audioSource.Length; ++j)
        {
            AudioSource childAC = audioSource[j];
            if (childAC.isPlaying)
            {
                StartCoroutine(FadeOut(childAC));
            }
        }
    }
    //fade Out the audioSource volume
    public static IEnumerator FadeOut(AudioSource sound)
    {
        float startVolume = sound.volume;
        float frameCount = 2.0f / Time.deltaTime;
        float framesPassed = 0;


        while (framesPassed <= frameCount)
        {
            ++framesPassed;
            float t = framesPassed / frameCount;
            sound.volume = Mathf.Lerp(startVolume, 0, t);
            yield return null;
        }

        //reset the volume and stop the audio as it ends
        sound.volume = startVolume;
        sound.Stop();
    }

    //Fade In AudioSource Volume
    public static IEnumerator FadeIn(AudioSource sound)
    {
        //play the sound immediately so it can start fading in
        sound.Play();
        float resultVolume = sound.volume;
        sound.volume = 0;
        float frameCount = 2.0f / Time.deltaTime;
        float framesPassed = 0;

        while (framesPassed <= frameCount)
        {
            ++framesPassed;
            float t = framesPassed / frameCount;
            sound.volume = Mathf.Lerp(0, resultVolume, t);
            yield return null;
        }

        sound.volume = resultVolume;
    }
}
