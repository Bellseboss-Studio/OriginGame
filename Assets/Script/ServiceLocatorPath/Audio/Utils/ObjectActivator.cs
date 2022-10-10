using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAudio
{
    

    public class ObjectActivator : MonoBehaviour, ICheckDependencies
    {
        [SerializeField] private BoxCollider m_Collider = null;
        [SerializeField] private Transform m_target;
        [SerializeField] public LayerMask obstacleLayers;
        [SerializeField] private bool m_IsRayHitting;
        [SerializeField] private GameObject[] m_Emitters;

        void Start()
        {
            CheckDependencies();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                foreach (GameObject emitter in m_Emitters)
                {
                    emitter.SetActive(true);
                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            foreach (GameObject emitter in m_Emitters)
            {
                emitter.SetActive(false);
            }
        }

        public void CheckDependencies()
        {
            if (m_Collider == null)
            {
                m_Collider = GetComponent<BoxCollider>();
            }
        }

        #region TRASH CODE

        private void Update()
        {
            // RaycastHit hit = new RaycastHit();
            // Ray ray = new Ray(this.transform.position, m_target.position - transform.position);
            // lr.SetPosition(0, transform.position);
            //
            // Debug.DrawRay(this.transform.position, m_target.position - transform.position, Color.red, 0.5f, true);
            // //float l = Vector3.Magnitude( m_target.position - transform.position);
            // rayray = Physics.Raycast(ray, out hit, 10f, obstacleLayers);
            // if (rayray)
            // {
            //    lr.SetPosition(1, hit.point);
            //   // Debug.Log(l);
            // }
            // else
            // {
            //    lr.SetPosition(1, m_target.position);
            //    Debug.Log("MANO");
            // }
        }

        #endregion
    }
}
