using UnityEngine;

public class Presentation : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    private bool _finishPresentation = true;
    
    public bool IsFinishPresentation => _finishPresentation;
    
    public virtual void StartPresentation()
    {
        _finishPresentation = false;
        animator.SetTrigger("start");
    }

    public virtual void FinishPresentation()
    {
        _finishPresentation = true;
    }

    public void StopAnimation()
    {
        _finishPresentation = true;
        animator.SetTrigger("stop");
    }
}