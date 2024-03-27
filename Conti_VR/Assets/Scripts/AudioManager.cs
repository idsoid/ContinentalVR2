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
    private List<string> audiosPlayed = new();

    void Start()
    {
        audiosToPlay.Clear();
        audiosPlayed.Clear();
        //WelcomeAudio();
    }

    void Update()
    {
        foreach (var audio in audioSources)
        {
            audio.mute = mute.isOn;
        }

        foreach (var audio in audioSources)
        {
            if (audio.isPlaying)
            {

            }
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
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Advance 1":
                            audiosToPlay = new() { "EBasicAdvance1", "EMeterBasic", "ECIDAdvance1", "ECIDAdvanceQuestion" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Advance 2":
                            audiosToPlay = new() { "EBasicAdvance2", "EMeterBasic", "ECIDAdvance2" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Premium":
                            audiosToPlay = new() { "EBasicPremium", "EMeterBasic", "ECIDPremium" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
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
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Advance 1":
                            audiosToPlay = new() { "EAdvanceAdvance1", "EMeterAdvance", "ECIDAdvance1", "ECIDAdvanceQuestion" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Advance 2":
                            audiosToPlay = new() { "EAdvanceAdvance2", "EMeterAdvance", "ECIDAdvance2" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Premium":
                            audiosToPlay = new() { "EAdvancePremium", "EMeterAdvance", "ECIDPremium" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
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
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Advance 1":
                            audiosToPlay = new() { "EPremiumActive1", "EMeterPremium", "ECIDAdvance1", "ECIDAdvanceQuestion" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Advance 2":
                            audiosToPlay = new() { "EPremiumActive2", "EMeterPremium", "ECIDAdvance2" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Premium":
                            audiosToPlay = new() { "EPremiumPremium", "EMeterPremium", "ECIDPremium" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
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
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Advance 1":
                            audiosToPlay = new() { "JBasicAdvance1", "JMeterBasic", "JCIDAdvance1", "JCIDAdvanceQuestion" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Advance 2":
                            audiosToPlay = new() { "JBasicAdvance2", "JMeterBasic", "JCIDAdvance2" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Premium":
                            audiosToPlay = new() { "JBasicPremium", "JMeterBasic", "JCIDPremium" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
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
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Advance 1":
                            audiosToPlay = new() { "JAdvanceAdvance1", "JMeterAdvance", "JCIDAdvance1", "JCIDAdvanceQuestion" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Advance 2":
                            audiosToPlay = new() { "JAdvanceAdvance2", "JMeterAdvance", "JCIDAdvance2" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Premium":
                            audiosToPlay = new() { "JAdvancePremium", "JMeterAdvance", "JCIDPremium" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
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
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Advance 1":
                            audiosToPlay = new() { "JPremiumActive1", "JMeterPremium", "JCIDAdvance1", "JCIDAdvanceQuestion" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Advance 2":
                            audiosToPlay = new() { "JPremiumActive2", "JMeterPremium", "JCIDAdvance2" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
                            break;
                        case "Premium":
                            audiosToPlay = new() { "JPremiumPremium", "JMeterPremium", "JCIDPremium" };
                            StartCoroutine(PlayAudioSequence(audiosToPlay));
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
            //if (audiosPlayed.Contains(audiosQueued[i].gameObject.name))
            //{
            //    break;
            //}

            audiosQueued[i].Play();
            //audiosPlayed.Add(audiosQueued[i].gameObject.name);
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
        foreach (var audio in audioSources)
        {
            if (audio.isPlaying)
            {
                audio.Stop();
                break;
            }
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
