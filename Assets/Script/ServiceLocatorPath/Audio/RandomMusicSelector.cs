using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameAudio
{
    public class RandomMusicSelector : MonoBehaviour
    {

        [SerializeField] private AudioSource m_AudioSource = null;

        [SerializeField] private List<AudioClip> m_MusicTracks = new List<AudioClip>();
   
        void Start()
        {
            CheckDependencies();
        }

        private void OnEnable()
        {
            StartCoroutine(nameof(SelectRandomAudioClip));
        }

        private void CheckDependencies()
        {
            if (m_AudioSource == null)
            {
                m_AudioSource = GetComponent<AudioSource>();
            }
        }

        IEnumerator SelectRandomAudioClip()
        {
            int randomTrack = Random.Range(0, m_MusicTracks.Count);
            m_AudioSource.clip = m_MusicTracks[randomTrack];
            m_AudioSource.Play();
            yield return new WaitForSeconds(m_MusicTracks[randomTrack].length);
            StartCoroutine(SelectRandomAudioClip());
        }
    }
}
