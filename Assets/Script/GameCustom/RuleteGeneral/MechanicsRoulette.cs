using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MechanicsRoulette : MonoBehaviour
{
    [SerializeField] private Animator animRoulette;
    [SerializeField] private int fiftyPercent;//50
    [SerializeField] private int thirtyPercent;//30
    [SerializeField] private int fifteenPercent;//15
    [SerializeField] private int fivePercent;//5
    [SerializeField] private List<TextMeshProUGUI> texts;
    [SerializeField] private TextMeshProUGUI winText;
    private List<int> multiplier;

    private void Start()
    {
        multiplier = new List<int>
        {
            fiftyPercent,
            thirtyPercent,
            fifteenPercent,
            fivePercent
        };
        for (int i = 0; i < texts.Count; i++)
        {
            texts[i].text = $"X {multiplier[i]}";
        }
    }
}
