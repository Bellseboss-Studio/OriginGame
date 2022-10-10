using System;
using SystemOfExtras;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameAudio
{
    public class PlayButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] protected TypeOfButton m_TypeOfButton = default;
        [Range(0f,1f)]
        [SerializeField] protected float m_ButtonSoundVolume = 0;
        
        [SerializeField] protected TypeOfButton m_WhooshSound = TypeOfButton.WhooshSfx;
        [Range(0f,0.5f)]
        [SerializeField] protected float m_WhooshVolume = 0;
        
        public virtual void OnButtonTappedSound()
        {
            if(ServiceLocator.Instance.GetService<UISounds>().isActiveAndEnabled)
            {
               // UISounds.Instance.OnButtonTappedSound();
                ServiceLocator.Instance.GetService<UISounds>().OnButtonTappedSound(m_TypeOfButton.ToString(), m_ButtonSoundVolume);
            }
            else
            {
                Debug.Log("No instance of UISounds is present in this scene.");
            }
        }
        
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            ServiceLocator.Instance.GetService<UISounds>().OnButtonTappedSound(m_WhooshSound.ToString(), m_WhooshVolume);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            return;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnButtonTappedSound();
        }
    }
}