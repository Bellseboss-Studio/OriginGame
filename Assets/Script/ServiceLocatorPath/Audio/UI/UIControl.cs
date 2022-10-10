using System;
using System.Collections.Generic;
using SystemOfExtras;
using UnityEngine;

namespace GameAudio
{
    public class UIControl : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_UIElements;
        private Dictionary<string, GameObject> m_UiElementsDictionary = new Dictionary<string, GameObject>();
        private bool m_IsGamePaused;

        private void Awake()
        {
            Application.targetFrameRate = 30;
            Debug.Log(">>>>>>>limit fps");
            ServiceLocator.Instance.RegisterService(this);
        }

        private void Start()
        {
            foreach (GameObject t in m_UIElements)
            {
                m_UiElementsDictionary.Add(t.gameObject.name, t);
            }
        }
        

        public void ActivateUIPannel(string panelOb)
        {
            if (m_UiElementsDictionary.ContainsKey(panelOb))
            {
                m_UiElementsDictionary[panelOb].SetActive(true);
            }

        }
        public void HideUI()
        {
            foreach (GameObject panel in m_UIElements)
            {
                panel.gameObject.SetActive(false);
            }
        }
    }
}
   
