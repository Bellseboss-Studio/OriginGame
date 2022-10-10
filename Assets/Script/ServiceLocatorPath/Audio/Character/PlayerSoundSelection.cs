using System.Collections.Generic;
using GameAudio;
using SystemOfExtras;
using UnityEngine;

namespace GameAudio
{
  public class PlayerSoundSelection : MonoBehaviour, ICheckDependencies
  {
    [SerializeField] private CapsuleCollider m_PlayerCollider;
    [SerializeField] private string m_CurrentMaterial = null;

    [SerializeField] private bool _isGrounded;
    [SerializeField] private float maxDistance;

    public bool IsGrounded => _isGrounded;
    
    public void PlayFootstepSound()
    {
      ServiceLocator.Instance.GetService<FootstepSoundSelection>().OnFootstep(m_CurrentMaterial.Replace("(Instance)", "").Trim());
    }
    
    public void CheckDependencies()
    {
      if (m_PlayerCollider == null)
      {
        m_PlayerCollider = GetComponentInChildren<CapsuleCollider>();
      }
    }
    private void Update()
    {
      Ray ray = new Ray(this.transform.position,Vector3.down);
      RaycastHit hit;
      Debug.DrawRay(transform.position, Vector3.down, Color.green);
      if (Physics.Raycast(ray,out hit, maxDistance ))
      {
        if (m_CurrentMaterial != hit.transform.gameObject.GetComponent<MeshRenderer>().material.name)
        {
          m_CurrentMaterial = hit.transform.gameObject.GetComponent<MeshRenderer>().material.name;
        }
        _isGrounded = true;
      }
      else
      {
        _isGrounded = false;
      }
    }
  }
}

