using System;
using System.Collections;
using System.Collections.Generic;
using GameAudio;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class FootstepSound : MonoBehaviour, ICheckDependencies
{
    [SerializeField] private List<AudioClip> m_AudioClips = new List<AudioClip>();

    [SerializeField] private float m_InitialPitch = 1;
    [Range(0f, 1f)]
    [SerializeField] private float m_MaxVolume = 1;
    
    [Range(0f, 0.5f)]
    [SerializeField] private float m_VolumeVariance;
    
    [Range(0f, 0.5f)]
    [SerializeField] private float m_PitchVariance;
    
    [SerializeField] private AudioSource m_As = null;
    void Start()
    {
        CheckDependencies();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(PlayFootstepSound));
    }

    public IEnumerator PlayFootstepSound()
    {
        int index = Random.Range(0, m_AudioClips.Count);
        AudioClip clip = m_AudioClips[index];
        m_As.pitch = Random.Range(m_InitialPitch - m_PitchVariance, m_InitialPitch + m_PitchVariance);
        m_As.volume = Random.Range(m_MaxVolume - m_VolumeVariance, m_MaxVolume);
        m_As.PlayOneShot(clip);
        yield return new WaitForSeconds(m_AudioClips[index].length);
        this.gameObject.SetActive(false);
    }

    public void CheckDependencies()
    {
        if (m_As == null)
        {
            m_As = GetComponent<AudioSource>();
        }
    }
}
