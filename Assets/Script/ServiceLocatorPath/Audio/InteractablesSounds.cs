using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using SystemOfExtras;

namespace GameAudio
{
    public class InteractablesSounds : MonoBehaviour
    {
        [SerializeField] private Transform[] m_SoundObjects;
        private Dictionary<string, GameObject> m_AudioObjectsDictionary = new Dictionary<string, GameObject>();


        private void Start()
        {
            ServiceLocator.Instance.RegisterService(this);
            AddItemsToDictionary();
        }

        public void PlaySound(string name)
        {
            StartCoroutine(PlaySoundCorroutine(name));
        }
        
        IEnumerator PlaySoundCorroutine(string name)
        {
            if(m_AudioObjectsDictionary.TryGetValue(name, out var go))
            {
                go.SetActive(true);
                yield return new WaitForSeconds(go.GetComponent<AudioSource>().clip.length);
                go.SetActive(false);   
            }
            else
            {
                Debug.Log($"Se acciono: {name}");
            }
        }
        
        private void AddItemsToDictionary()
        {
            m_SoundObjects = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                m_SoundObjects[i] = transform.GetChild(i);
            }
            foreach (Transform t in m_SoundObjects)
            {
                m_AudioObjectsDictionary.Add(t.gameObject.name, t.gameObject);
            }
        }
    }
}