using UnityEngine;

public class ShowMessageFromGamePresentation : MonoBehaviour
{
    [SerializeField] private GeneralBlackJack messageGBJ;

    public void DontExceed32()
    {
        messageGBJ.ShowMessage("How to Play?", "Whoever exceeds 32 points, will lose the game");
    }

    public void YouCanPlace()
    {
        messageGBJ.ShowMessage("Place", "move the cards to the center for adding 1 charge");
    }

    public void YouCanPass()
    {
        messageGBJ.ShowMessage("Pass", "Just move the card to the back of the table");
    }
    
    public void YouCanSet()
    {
        messageGBJ.ShowMessage("Set", "Move the card To the altar. Adds +2 to your charge but you won't be able to pass again in that round. Use it wisely.");
    }

    public void WhenLose()
    {
        messageGBJ.ShowMessage("When someone loses", "will also receive multiplied damage, according by the opponent's previous charge");
    }
}