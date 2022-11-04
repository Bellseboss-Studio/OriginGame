using Gameplay.UsoDeCartas;
using TMPro;
using UnityEngine;

public class CardInWord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textOfNumber;
    [SerializeField] private DragComponent drag;
    private Card _card;
    public Card Card => _card;

    public void Configurate(Card card, Camera camera, IDeckForGame deckForGame)
    {
        _card = card;
        textOfNumber.text = $"{_card.number}";
        drag.Configure(camera, deckForGame);
    }
}