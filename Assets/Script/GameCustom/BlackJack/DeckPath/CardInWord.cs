using TMPro;
using UnityEngine;

public class CardInWord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textOfNumber;
    private Card _card;
    public Card Card => _card;

    public void Configurate(Card card)
    {
        _card = card;
        textOfNumber.text = $"{_card.number}";
    }
}