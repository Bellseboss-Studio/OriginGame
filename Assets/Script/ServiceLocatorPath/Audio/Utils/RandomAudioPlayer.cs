using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Audio;


namespace GameAudio
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomAudioPlayer : MonoBehaviour
    {
        #region variables

        [Header("Sound Collection")] [SerializeField]
        private List<AudioClip> m_SoundsToPlay;

        [SerializeField] private AudioSource m_As;
        [SerializeField] private float m_MinDelayTime;
        [SerializeField] private float m_MaxDelayTime;
        [SerializeField] private float m_MaxVolume;
        [SerializeField] private float m_StereoPan;
        [SerializeField] private bool m_IsLoopable = false;
        [SerializeField] private bool m_UseClipsLength = false;
        private const float m_NormalPitch = 1f;

        [Range(0f, 0.7f)] [SerializeField] private float m_VolumeVariance;
        [Range(0f, 0.3f)] [SerializeField] private float m_PitchVariance;
        [Range(0f, 1f)] [SerializeField] private float m_PositionVariance;

        #endregion

        void Start()
        {
            CheckForDependencies();
            m_StereoPan = m_As.panStereo;
        }

        private void OnEnable()
        {
            StartCoroutine(nameof(PlayAudioClip));
        }

        private IEnumerator PlayAudioClip()
        {
            m_As.clip = m_SoundsToPlay[Random.Range(0, m_SoundsToPlay.Count)];
            m_As.volume = Random.Range(m_MaxVolume - m_VolumeVariance, m_MaxVolume);
            m_As.pitch = Random.Range(m_NormalPitch - m_PitchVariance, m_NormalPitch + m_PitchVariance);
            m_As.panStereo = Random.Range(m_StereoPan - m_PositionVariance, m_StereoPan + m_PositionVariance);
            m_As.Play();
            if (m_IsLoopable)
            {
                float delayTime = Random.Range(m_MinDelayTime, m_MaxDelayTime);
                if (m_UseClipsLength)
                {
                    yield return new WaitForSeconds(m_As.clip.length);
                }
                else
                {
                    yield return new WaitForSeconds(delayTime);
                }

                StartCoroutine(nameof(PlayAudioClip));
            }
            else
            {
                yield return null;
            }

        }

        private void CheckForDependencies()
        {
            if (m_As == null)
            {
                m_As = GetComponent<AudioSource>();
            }

            if (m_As.outputAudioMixerGroup == null)
            {
                Debug.LogError($"AudioOutput on this gameObject: {this.gameObject.name} needs to be assigned");
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(nameof(PlayAudioClip));
        }
    }
}