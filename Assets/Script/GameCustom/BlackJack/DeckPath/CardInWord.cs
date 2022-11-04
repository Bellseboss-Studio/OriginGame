using Gameplay.UsoDeCartas;
using TMPro;
using UnityEngine;

public class CardInWord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textOfNumber;
    [SerializeField] private DragComponent drag;
    private Card _card;
    public Card Card => _card;

    public void Configurate(Card card)
    {
        _card = card;
        textOfNumber.text = $"{_card.number}";
    }

    public void EnabledDragComponent(Camera camera, IDeckForGame deckForGame)
    {
        drag.Configure(camera, deckForGame);
    }
}