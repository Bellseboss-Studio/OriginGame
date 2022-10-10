
using UnityEngine;


namespace GameAudio
{
    public class GameObjectNameAssignation : MonoBehaviour
    {
        [SerializeField] private string m_ObjectName = null;
        void Awake()
        {
            if (m_ObjectName != null)
            {
                gameObject.name = m_ObjectName;
            }
           
        }
    
    } 
}

