using System;
using System.Collections;
using System.Collections.Generic;
using SystemOfExtras;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameAudio
{
    public class ClickUIButton : MonoBehaviour
    {
        private UIControl m_UiControl;
        [SerializeField] private float m_WaitingTimeToLoad = 1f;
        private void Start()
        {
            m_UiControl = ServiceLocator.Instance.GetService<UIControl>();
        }

        public void StartGame()
        {
            StartCoroutine(nameof(LoadGame));
        }

        IEnumerator LoadGame()
        {
            SceneManager.LoadScene(1);
            yield return new WaitForSeconds(m_WaitingTimeToLoad);
            ExitUi();
            
        }

        public void ExitUi()
        {
            m_UiControl.HideUI();
        }

        public void ActivateScreen(string panel)
        {
            m_UiControl.ActivateUIPannel(panel);
        }
        
    }
}
