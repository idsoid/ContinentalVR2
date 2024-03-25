using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject questionPopup;
    [SerializeField]
    private List<AudioSource> audioSources = new();
    private List<string> audiosToPlay;

    void Start()
    {
        foreach (var audio in audioSources)
        {
            if (audio.isPlaying)
            {
                audio.Stop();
            }
        }
        WelcomeAudio();
    }

    private void WelcomeAudio()
    {
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
    public IEnumerator PlayAudioSequence(List<string> audiosToPlay)
    {
        List<AudioSource> audiosQueued = new();
        for (int i = 0; i < audiosToPlay.Count; i++)
        {
            for (int j = 0; j < audioSources.Count; j++)
            {
                if (audiosToPlay[i] == audioSources[j].gameObject.name)
                {
                    audiosQueued.Add(audioSources[j].GetComponent<AudioSource>());
                    break;
                }
            }
        }
        Debug.Log(audiosQueued.Count);

        for (int i = 0; i < audiosQueued.Count; i++)
        {
            audiosQueued[i].PlayDelayed(1.0f);
            yield return new WaitForSeconds(audiosQueued[i].clip.length + 1.5f);
            if (audiosQueued[i].name == "ECIDAdvance1" || audiosQueued[i].name == "JCIDAdvance1")
            {
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
            }
        }

        //Play audio by name
        foreach (var audio in audioSources)
        {
            if (audio.gameObject.name == audioName)
            {
                audio.PlayDelayed(1.0f);
                break;
            }
        }
    }
}
