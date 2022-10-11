using UnityEngine;
using System.Collections.Generic;
using SystemOfExtras;

namespace GameAudio
{
    [RequireComponent(typeof(AudioSource))]
    public class UISounds : MonoBehaviour, ICheckDependencies

    {
        [SerializeField] private AudioSource m_AudioSource = null;
        [SerializeField] private AudioClip[] m_UIAudioFiles = null;
        private Dictionary<string, AudioClip> m_UIAudioFilesDictionary = new Dictionary<string, AudioClip>();

        private void Start()
        {
            CheckDependencies();
            AddItemsToDictionary();
        }

        public void OnButtonTappedSound(string buttonName, float volume)
        {
            if (m_UIAudioFilesDictionary.ContainsKey(buttonName))
            {
                m_AudioSource.volume = volume;
                m_AudioSource.PlayOneShot(m_UIAudioFilesDictionary[buttonName]);
            }
            else
            {
                Debug.Log(
                    $"{buttonName} not present in UISounds dictionary or no instance of UISounds is present in the scene. " +
                    $"Instantiate the UISounds prefab or play from Main Menu to listen to UI Sounds");
            }
        }

        private void AddItemsToDictionary()
        {
            foreach (AudioClip audioClip in m_UIAudioFiles)
            {
                m_UIAudioFilesDictionary.Add(audioClip.name, audioClip);
            }
        }

        public void CheckDependencies()
        {
            if (m_AudioSource == null)
            {
                m_AudioSource = GetComponent<AudioSource>();
            }
            
            ServiceLocator.Instance.RegisterService(this);
        }
    }
}


#region Old Code

//[SerializeField] private Transform[] m_UIAudioObjects = null;


// if (!m_UIAudioFilesDictionary[buttonName].activeInHierarchy)
// { 
//     m_UIAudioFilesDictionary[buttonName].gameObject.SetActive(true);
// }

// m_UIAudioObjects = new Transform[transform.childCount];
// for (int i = 0; i < m_UIAudioFiles.Length; i++)
// {
//     m_UIAudioObjects[i] = transform.GetChild(i);
// }
// foreach (Transform t in m_UIAudioObjects)
// {
//     m_UIAudioObjectsDictionary.Add(t.gameObject.name, t.gameObject);
// }

#endregion