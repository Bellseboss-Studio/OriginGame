using UnityEngine;

public class ShowMessageFromGamePresentation : MonoBehaviour
{
    [SerializeField] private GeneralBlackJack messageGBJ;

    public void DontExceed32()
    {
        messageGBJ.ShowMessage("How to Play?", "loses who exceeds 32");
    }

    public void YouCanPlace()
    {
        messageGBJ.ShowMessage("Place", "Move the card to the center of the table and add +1 Load");
    }

    public void YouCanPass()
    {
        messageGBJ.ShowMessage("Pass", "Move the card to the back of the table and it does not add to the load");
    }
    
    public void YouCanSet()
    {
        messageGBJ.ShowMessage("Set", "Move the card to the altar of the hand, add +2 load and don't pass again in this round");
    }

    public void WhenLose()
    {
        messageGBJ.ShowMessage("When somebody lose", "receives damage multiplied by the opponent's loads");
    }
}