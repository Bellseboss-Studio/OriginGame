using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAudio
{
    public class PlayMultipleAnimationSounds : MonoBehaviour
    {
        [SerializeField] private bool m_CheckForChildren;
        [SerializeField] private Transform[] m_SoundObjects = null;
        private Dictionary<string, GameObject> m_AudioObjectsDictionary = new Dictionary<string, GameObject>();
        private void Start()
        {
            CheckDependencies();
        }
        public void PlaySound(string go)
        {
            if (m_AudioObjectsDictionary.ContainsKey(go))
            {
                StartCoroutine(PlaySoundCorroutine(go));
            }
            else
            {
                Debug.Log($"{go} could not be found fo {this.gameObject.name}");
            }
            
        }
        public void CheckDependencies()
        {
            AddItemsToDictionary();
        }
        IEnumerator PlaySoundCorroutine(string name)
        {
            GameObject go = m_AudioObjectsDictionary[name];
            go.SetActive(true);
            yield return new WaitForSeconds(go.GetComponent<AudioSource>().clip.length);
            go.SetActive(false);
        }
        private void AddItemsToDictionary()
        {
            if (m_CheckForChildren)
            {
                m_SoundObjects = new Transform[transform.childCount];
                for (int i = 0; i < transform.childCount; i++)
                {
                    m_SoundObjects[i] = transform.GetChild(i);
                }
            }
            
            foreach (Transform t in m_SoundObjects)
            {
                m_AudioObjectsDictionary.Add(t.gameObject.name, t.gameObject);
            }
        }
    } 
}
