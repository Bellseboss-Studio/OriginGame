using TMPro;
using UnityEngine;

public class UIButtonAssignation : MonoBehaviour
{
    [SerializeField] private string m_ButtonName = null;
    [SerializeField] private TMP_Text m_ButtonText = null;
    void Start()
    {
        m_ButtonText = GetComponentInChildren<TMP_Text>();
        m_ButtonText.text = m_ButtonName;
    }
    
}
