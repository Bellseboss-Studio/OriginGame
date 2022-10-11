using UnityEngine;

namespace GameAudio
{
    public class SetMasterFader : SetFaderLevel
    {
        private void Awake()
        {
            m_StartingValue = m_Slider.value;
        }
        public override void SetVolume(float sliderValue)
        {
            m_Mixer.SetFloat(m_FaderToControl, Mathf.Log10(sliderValue) * 20);
            UIPerdurableValues.MasterFaderValue = m_Slider.value;
        }

        private void OnEnable()
        {
            m_Slider.value = UIPerdurableValues.MasterFaderValue;
        }
    }
}