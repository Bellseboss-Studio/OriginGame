using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Gameplay
{
    public class MixerManager : MonoBehaviour
    {
        [SerializeField] private AudioMixerSnapshot m_GameplayMix = null;
        [SerializeField] private AudioMixerSnapshot m_PauseMix = null;
        [SerializeField] private float m_TransitionTime = 0.5f;
        private void Start()
        {
            CheckDependencies();
        }

        public void SelectMixerSnapshot(bool isPaused)
        {
            if (isPaused)
            {
                m_PauseMix.TransitionTo(m_TransitionTime);
            }
            else
            {
                m_GameplayMix.TransitionTo(m_TransitionTime);
            }
        }

        void CheckDependencies()
        {
            if (m_GameplayMix == null || m_PauseMix == null)
            {
                Debug.Log("One or More AudioMixerSnapshots need to be assigned");
            }
        }
    } 
}

