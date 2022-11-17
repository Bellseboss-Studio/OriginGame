using UnityEngine;
public class PresentationAnimationInTutorial : Presentation
{
    [SerializeField] protected ShowMessageTransitionally messages;

    public override void StartPresentation()
    {
        base.StartPresentation();
        messages.OnClickInScream += NextAnimation;
    }

    public override void FinishPresentation()
    {
        base.FinishPresentation();
        messages.OnClickInScream -= NextAnimation;
    }

    public void NextAnimation()
    {
        animator.SetTrigger("next");
    }
}