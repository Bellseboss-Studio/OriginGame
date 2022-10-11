using System.Collections;
using System.Collections.Generic;
using SystemOfExtras;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GameAudio
{
    public class SetFaderLevel : MonoBehaviour
    {
        [SerializeField] protected Slider m_Slider;
        [SerializeField] protected string m_FaderToControl;
        [SerializeField] protected float m_StartingValue = 1f;
        [SerializeField] protected float m_CurrentValue;

        [SerializeField] protected AudioMixer m_Mixer;

        private void Awake()
        {
            m_Slider.minValue = 0.0001f;
            CheckDependencies();
            DontDestroyOnLoad(this.gameObject);
        }

        public void CheckDependencies()
        {
            if (m_Slider == null)
            {
                m_Slider = GetComponent<Slider>();
            }

            if (m_FaderToControl == null)
            {
                m_FaderToControl = this.gameObject.name.ToString();
            }

            if (m_Mixer == null)
            {
                Debug.Log($"No audio mixer has been assigned for this {gameObject.name}");
            }
        }

        public virtual void SetVolume(float sliderValue)
        {
            if (m_Mixer != null)
            {
                m_Mixer.SetFloat(m_FaderToControl, Mathf.Log10(sliderValue) * 20);
                m_CurrentValue = m_Slider.value;
            }
        }
    }
}
