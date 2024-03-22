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
    private List<AudioSource> audioSources = new();

    void Start()
    {
        if (PlayerPrefsManager.Load("Cluster") == "Affordable")
        {
            switch (PlayerPrefsManager.Load("CID"))
            {
                case "Affordable":
                    PlayAudio("BasicBasic");
                    break;
                case "Advance 1":
                    PlayAudio("BasicAdvance1");
                    break;
                case "Advance 2":
                    PlayAudio("BasicAdvance2");
                    break;
                case "Premium":
                    PlayAudio("BasicPremium");
                    break;
                default:
                    break;
            }
        }
        else if (PlayerPrefsManager.Load("Cluster") == "Advance")
        {
            switch (PlayerPrefsManager.Load("CID"))
            {
                case "Affordable":
                    PlayAudio("AdvanceBasic");
                    break;
                case "Advance 1":
                    PlayAudio("AdvanceAdvance1");
                    break;
                case "Advance 2":
                    PlayAudio("AdvanceAdvance2");
                    break;
                case "Premium":
                    PlayAudio("AdvancePremium");
                    break;
                default:
                    break;
            }
        }
        else if (PlayerPrefsManager.Load("Cluster") == "Premium")
        {
            switch (PlayerPrefsManager.Load("CID"))
            {
                case "Affordable":
                    PlayAudio("PremiumBasic");
                    break;
                case "Advance 1":
                    PlayAudio("PremiumActive1");
                    break;
                case "Advance 2":
                    PlayAudio("PremiumActive2");
                    break;
                case "Premium":
                    PlayAudio("PremiumPremium");
                    break;
                default:
                    break;
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
