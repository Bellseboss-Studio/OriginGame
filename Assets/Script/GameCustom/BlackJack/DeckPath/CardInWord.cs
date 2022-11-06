using Gameplay.UsoDeCartas;
using TMPro;
using UnityEngine;

public class CardInWord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textOfNumber;
    [SerializeField] private DragComponent drag;
    private Card _card;
    private IGameLogic _gameLogic;
    public Card Card => _card;

    public void Configurate(Card card, IGameLogic gameLogic)
    {
        _card = card;
        _gameLogic = gameLogic;
        textOfNumber.text = $"{_card.number}";
    }

    public void EnabledDragComponent(Camera camera, IDeckForGame deckForGame)
    {
        drag.Configure(camera, deckForGame, _gameLogic);
    }

    public void LeaveCard()
    {
        drag.enabled = false;
    }
}