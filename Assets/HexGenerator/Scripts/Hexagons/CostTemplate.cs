using TMPro;
using UnityEngine;

public class CostTemplate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cost;
    public void Using(string s)
    {
        cost.text = s;
    }
}