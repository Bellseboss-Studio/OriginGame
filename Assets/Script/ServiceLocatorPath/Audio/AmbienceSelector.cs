using System;
using System.Collections;
using System.Collections.Generic;
using SystemOfExtras;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AmbienceSelector : MonoBehaviour
{
  
    [SerializeField] private Transform[] m_AmbienceSounds = null;
    
    [SerializeField] private float m_CrossfadeTime;
    
    [SerializeField] private AudioMixerSnapshot[] m_MixerSnapshots;
    
    private Dictionary<string, GameObject>  m_AmbienceObjectDictionary = new Dictionary<string, GameObject>();
    private Dictionary<string, AudioMixerSnapshot> m_AmbiencesSnapshotDictionary = new Dictionary<string, AudioMixerSnapshot>();

    [SerializeField] string m_CurrentAmbient = "Room";
    [SerializeField] private string m_Buffer;
    private void Start()
    {
        ServiceLocator.Instance.RegisterService(this);
        
        foreach (Transform t in m_AmbienceSounds)
        {
            m_AmbienceObjectDictionary.Add(t.name, t.gameObject);
        }
        
        foreach (AudioMixerSnapshot ss in m_MixerSnapshots)
        {
            m_AmbiencesSnapshotDictionary.Add(ss.name, ss);
        }
    }

    public void SetAmbienceSound(string stringtag)
    {
        StartCoroutine(AmbienceSequence(stringtag));
    }

    IEnumerator AmbienceSequence(string currentTag)
    {
        if (m_CurrentAmbient != currentTag)
        {
            m_AmbienceObjectDictionary[currentTag].SetActive(true);
            m_AmbiencesSnapshotDictionary[currentTag].TransitionTo(m_CrossfadeTime);
            yield return new WaitForSeconds(m_CrossfadeTime);
            m_AmbienceObjectDictionary[m_CurrentAmbient].SetActive(false);
            m_CurrentAmbient = currentTag;  
        }
    }
    
}
