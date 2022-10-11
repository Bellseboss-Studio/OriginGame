using System;
using System.Collections;
using System.Collections.Generic;
using SystemOfExtras;
using UnityEngine;

public class AmbienceEmitter : MonoBehaviour
{
    [SerializeField] private string m_ColliderTag;
    private void Start()
    {
        m_ColliderTag = gameObject.tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ServiceLocator.Instance.GetService<AmbienceSelector>().SetAmbienceSound(m_ColliderTag);
        }
        
    }
}
