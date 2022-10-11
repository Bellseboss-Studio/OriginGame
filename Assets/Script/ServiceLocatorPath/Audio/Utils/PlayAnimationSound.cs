using System;
using GameAudio;
using UnityEngine;


namespace GameAudio
{
    public class PlayAnimationSound : MonoBehaviour, ICheckDependencies
    {
        [SerializeField] private AudioClip m_Sound = null;
        [SerializeField] private AudioSource m_As = null;

        [Header("Distance to Audio Listener")]
        [SerializeField] private bool m_CheckForDistance = true;
        [SerializeField] private float m_Magnitude;
        [SerializeField] private float m_MinDistance;
        
        
        [Tooltip("Transform of the AudioListener and the GameObject with the audio source")]
        [SerializeField] private Transform m_Target;
        [SerializeField] private GameObject m_Emitter;
        private void Start()
        {
            CheckDependencies();
            
        }
        public void PlaySound()
        {
            if (m_CheckForDistance)
            {
                m_Magnitude = Vector3.Magnitude(m_Target.position - this.gameObject.transform.position);
                if(m_Magnitude < m_MinDistance)
                {
                    m_Emitter.SetActive(true);
                    m_As.PlayOneShot(m_Sound);
                }
                else
                {  
                    m_Emitter.SetActive(false);
                }
            }
            else
            {
                m_Emitter.SetActive(true);
                m_As.PlayOneShot(m_Sound);
            }
            
        }

        public void CheckDependencies()
        {
            if (m_As == null)
            {
                m_As = GetComponentInChildren<AudioSource>();
            }
            m_Target = FindObjectOfType<AudioListener>().transform;
            m_Emitter = m_As.gameObject;
        }
    }
}