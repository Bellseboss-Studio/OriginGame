using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameAudio
{
    public class UISoundToPlay : MonoBehaviour
    {
        [SerializeField] private AudioSource m_AudioSource = null;
        [SerializeField] private AudioClip m_AudioClip = null;
        [Range(0f, 0.2f)]
        [SerializeField] private float m_Pitchvariation = 0;
        [Range (0f, 0.5f)]
        [SerializeField] private float m_InitialDelay = 0;
        private void Start()
        {
            CheckDependencies();
        }
        private void OnEnable()
        {
            StartCoroutine(nameof(ActivateGameObject));
        }
        private IEnumerator ActivateGameObject()
        {
            m_AudioSource.pitch = Random.Range(1f - m_Pitchvariation, 1f + m_Pitchvariation);
            yield return new WaitForSeconds(m_InitialDelay);
            if (m_AudioSource.isPlaying)
            {
                m_AudioSource.Stop();
            }
            m_AudioSource.PlayOneShot(m_AudioClip);
            yield return new WaitForSeconds(m_AudioClip.length);
            gameObject.SetActive(false);
        }
        private void CheckDependencies()
        {
            if (m_AudioSource == null)
            {
                m_AudioSource = GetComponent<AudioSource>();
            }

            if (m_AudioClip == null)
            {
                Debug.Log($"No AudioClip(s) present for this Game Object: {this.gameObject.name}");
            }
        }
    }
}
