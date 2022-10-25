using UnityEngine;

public abstract class DeckForGame : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    private static readonly int Draw = Animator.StringToHash("draw");
    protected bool _drawIsFinished;

    public virtual void DrawCards()
    {
        animator.SetTrigger(Draw);
    }

    protected void FinishToDraw()
    {
        _drawIsFinished = true;
    }
    
    public bool DrawIsFinished => _drawIsFinished;
}