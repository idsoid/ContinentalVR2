using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton code
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

    }
    #endregion
    struct AudioParams
    {
        //you can write the name of the audio you are refering to
        public string audioName;
        public AudioClass m_AudioClass;
        //constructor
        public AudioParams(string _name, AudioClass _audioClass)
        {
            audioName = _name;
            m_AudioClass = _audioClass;
        }
    }
    List<AudioParams> audioList = new List<AudioParams>();
    // Start is called before the first frame update
    void Start()
    {
        //on game start get all child audioClasses
        for (int i = 0; i < transform.childCount; ++i)
        {
            AudioClass childAC = transform.GetChild(i).GetComponent<AudioClass>();
            audioList.Add(new AudioParams(childAC.audioName, childAC));
        }
    }
    //play a specific audio by name
    public void PlayAudio(string _audioName)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (audioList[i].audioName != _audioName)
                continue;
            audioList[i].m_AudioClass.PlayAudio();
        }
    }
    //stop a specifc audio by name
    public void StopAudio(string _audioName)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (audioList[i].audioName != _audioName)
                continue;
            audioList[i].m_AudioClass.StopAudio();
        }
    }
    //stop all active audios
    public void StopAllAudio()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            audioList[i].m_AudioClass.StopAudio();
        }
    }
}
