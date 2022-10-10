using System;
using UnityEngine;
using System.Collections.Generic;
using SystemOfExtras;

namespace GameAudio
{
    public class FootstepSoundSelection : MonoBehaviour
    {
        [SerializeField] private Transform[] m_FsAudioObjects = null;
        
        private Dictionary<string, GameObject> m_UIAudioObjectsDictionary = new Dictionary<string, GameObject>();
        
        private void Start()
        {
            AddItemsToDictionary();
            ServiceLocator.Instance.RegisterService(this);
        }
        
        public void OnFootstep(string surface)
        {
            if (m_UIAudioObjectsDictionary.ContainsKey(surface))
            {
                if (!m_UIAudioObjectsDictionary[surface].activeInHierarchy)
                { 
                    m_UIAudioObjectsDictionary[surface].gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.Log(
                    $"{surface} not present in FsAudioObjects dictionary or no instance of Fs_Footsteps is present in the scene. ");
            }
        }
        private void AddItemsToDictionary()
        {
            m_FsAudioObjects = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                m_FsAudioObjects[i] = transform.GetChild(i);
            }
            foreach (Transform t in m_FsAudioObjects)
            {
                m_UIAudioObjectsDictionary.Add(t.gameObject.name, t.gameObject);
            }
        }

       
    }
}