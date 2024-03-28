using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }

    [SerializeField]
    private Toggle mute;
    [SerializeField]
    private GameObject questionPopup;
    [SerializeField]
    private List<AudioSource> audioSources = new();
    private List<string> audiosToPlay = new();
    private IEnumerator audioCoroutine;

    void Start()
    {
        audiosToPlay.Clear();
        WelcomeAudio();
        StartCoroutine(audioCoroutine);
    }

    void Update()
    {
        foreach (var audio in audioSources)
        {
            if (audio.isPlaying)
            {
                Debug.Log(audio.name);
            }
        }

        foreach (var audio in audioSources)
        {
            audio.mute = mute.isOn;
        }
    }

    private void WelcomeAudio()
    {
        foreach (var audio in audioSources)
        {
            if (audio.isPlaying)
            {
                audio.Stop();
            }
        }

        if (PlayerPrefsManager.Load("Language") == "English")
        {
            switch (PlayerPrefsManager.Load("Cluster"))
            {
                case "Basic":
                    switch (PlayerPrefsManager.Load("CID"))
                    {
                        case "Basic":
                            audiosToPlay = new() { "EBasicBasic", "EMeterBasic", "ECIDBasic" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Advance 1":
                            audiosToPlay = new() { "EBasicAdvance1", "EMeterBasic", "ECIDAdvance1", "ECIDAdvanceQuestion" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Advance 2":
                            audiosToPlay = new() { "EBasicAdvance2", "EMeterBasic", "ECIDAdvance2" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Premium":
                            audiosToPlay = new() { "EBasicPremium", "EMeterBasic", "ECIDPremium" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        default:
                            break;
                    }
                    break;
                case "Advance":
                    switch (PlayerPrefsManager.Load("CID"))
                    {
                        case "Basic":
                            audiosToPlay = new() { "EAdvanceBasic", "EMeterAdvance", "ECIDBasic" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Advance 1":
                            audiosToPlay = new() { "EAdvanceAdvance1", "EMeterAdvance", "ECIDAdvance1", "ECIDAdvanceQuestion" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Advance 2":
                            audiosToPlay = new() { "EAdvanceAdvance2", "EMeterAdvance", "ECIDAdvance2" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Premium":
                            audiosToPlay = new() { "EAdvancePremium", "EMeterAdvance", "ECIDPremium" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        default:
                            break;
                    }
                    break;
                case "Premium":
                    switch (PlayerPrefsManager.Load("CID"))
                    {
                        case "Basic":
                            audiosToPlay = new() { "EPremiumBasic", "EMeterPremium", "ECIDBasic" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Advance 1":
                            audiosToPlay = new() { "EPremiumActive1", "EMeterPremium", "ECIDAdvance1", "ECIDAdvanceQuestion" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Advance 2":
                            audiosToPlay = new() { "EPremiumActive2", "EMeterPremium", "ECIDAdvance2" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Premium":
                            audiosToPlay = new() { "EPremiumPremium", "EMeterPremium", "ECIDPremium" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        else if (PlayerPrefsManager.Load("Language") == "Japanese")
        {
            switch (PlayerPrefsManager.Load("Cluster"))
            {
                case "Basic":
                    switch (PlayerPrefsManager.Load("CID"))
                    {
                        case "Basic":
                            audiosToPlay = new() { "JBasicBasic", "JMeterBasic", "JCIDBasic" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Advance 1":
                            audiosToPlay = new() { "JBasicAdvance1", "JMeterBasic", "JCIDAdvance1", "JCIDAdvanceQuestion" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Advance 2":
                            audiosToPlay = new() { "JBasicAdvance2", "JMeterBasic", "JCIDAdvance2" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Premium":
                            audiosToPlay = new() { "JBasicPremium", "JMeterBasic", "JCIDPremium" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        default:
                            break;
                    }
                    break;
                case "Advance":
                    switch (PlayerPrefsManager.Load("CID"))
                    {
                        case "Basic":
                            audiosToPlay = new() { "JAdvanceBasic", "JMeterAdvance", "JCIDBasic" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Advance 1":
                            audiosToPlay = new() { "JAdvanceAdvance1", "JMeterAdvance", "JCIDAdvance1", "JCIDAdvanceQuestion" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Advance 2":
                            audiosToPlay = new() { "JAdvanceAdvance2", "JMeterAdvance", "JCIDAdvance2" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Premium":
                            audiosToPlay = new() { "JAdvancePremium", "JMeterAdvance", "JCIDPremium" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        default:
                            break;
                    }
                    break;
                case "Premium":
                    switch (PlayerPrefsManager.Load("CID"))
                    {
                        case "Basic":
                            audiosToPlay = new() { "JPremiumBasic", "JMeterPremium", "JCIDBasic" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Advance 1":
                            audiosToPlay = new() { "JPremiumActive1", "JMeterPremium", "JCIDAdvance1", "JCIDAdvanceQuestion" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Advance 2":
                            audiosToPlay = new() { "JPremiumActive2", "JMeterPremium", "JCIDAdvance2" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        case "Premium":
                            audiosToPlay = new() { "JPremiumPremium", "JMeterPremium", "JCIDPremium" };
                            audioCoroutine = PlayAudioSequence(audiosToPlay);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
    public void QueueAudios(List<string> audios)
    {
        StartCoroutine(PlayAudioSequence(audios));
    }
    public IEnumerator PlayAudioSequence(List<string> audiosToPlay)
    {
        //Check if an audio is playing
        StopCoroutine(audioCoroutine);
        for (int i = 0; i < audioSources.Count; i++)
        {
            audioSources[i].Stop();
        }

        List<AudioSource> audiosQueued = new();
        for (int i = 0; i < audiosToPlay.Count; i++)
        {
            for (int j = 0; j < audioSources.Count; j++)
            {
                if (audiosToPlay[i] == audioSources[j].gameObject.name)
                {
                    audiosQueued.Add(audioSources[j]);
                    break;
                }
            }
        }

        for (int i = 0; i < audiosQueued.Count; i++)
        {
            audiosQueued[i].Play();
            yield return new WaitForSecondsRealtime(audiosQueued[i].clip.length + 1f);
            if (audiosQueued[i].gameObject.name == "ECIDAdvance1" || audiosQueued[i].gameObject.name == "JCIDAdvance1")
            {
                Debug.Log("test");
                questionPopup.SetActive(true);
            }
        }
    }
    public void PlayAudio(string audioName)
    {
        //Check if an audio is playing
        StopCoroutine(audioCoroutine);
        for (int i = 0; i < audioSources.Count; i++)
        {
            audioSources[i].Stop();
        }

        //Play audio by name
        foreach (var audio in audioSources)
        {
            if (audio.gameObject.name == audioName)
            {
                audio.Play();
                break;
            }
        }
    }
}
