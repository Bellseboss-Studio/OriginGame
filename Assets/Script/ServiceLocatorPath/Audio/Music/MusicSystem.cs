using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemOfExtras;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicSystem : Singleton<MusicSystem>
{
    [SerializeField] private GameObject[] m_MxTracks;
    [SerializeField] private AudioMixerSnapshot[] m_MixerSnapshots;
   
    [SerializeField] private float m_CrossfadeTime;
    private int m_CurrentScene = 0;
    private float m_TransitionTimeToPause = 1f;
    private int m_Buffer;

    public override void Awake()
    {
      ServiceLocator.Instance.RegisterService(this);
      DontDestroyOnLoad(this);
    }

    void Start()
    {
        PlayMusicObject(m_CurrentScene);
    }
    void Update()
    {
        if (m_CurrentScene != SceneManager.GetActiveScene().buildIndex)
        {
            m_CurrentScene = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(SwitchMusicTrack(m_CurrentScene));
        }
    }
    public void PlayMusicObject(int scene)
    {
        foreach (var gameObject in m_MxTracks)
        {
            gameObject.SetActive(false);
        }
        m_MxTracks[m_CurrentScene].SetActive(true);
        m_MixerSnapshots[scene].TransitionTo(m_CrossfadeTime);
    }

    IEnumerator SwitchMusicTrack(int scene)
    {
        m_MxTracks[scene].SetActive(true);
        m_MixerSnapshots[scene].TransitionTo(m_CrossfadeTime);
        yield return new WaitForSeconds(m_CrossfadeTime);
        m_MxTracks[m_Buffer].SetActive(false);
        m_Buffer = m_CurrentScene;
    }

    public void SetPauseMixer()
    {
        m_MixerSnapshots[3].TransitionTo(m_TransitionTimeToPause);
       
    }
    
    public void UnpauseMixer()
    {
        m_MixerSnapshots[1].TransitionTo(m_TransitionTimeToPause);
     
    }
    
}
