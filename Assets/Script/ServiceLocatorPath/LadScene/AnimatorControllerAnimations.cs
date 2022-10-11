using UnityEngine;

public class AnimatorControllerAnimations : MonoBehaviour
{
    private bool isInAnimation;

    public bool IsInAnimation
    {
        get
        {
            if (!isInAnimation) return false;
            isInAnimation = false;
            return true;
        }
    }

    public void StartAnimation()
    {
        isInAnimation = false;
    }

    public void FinishedAnimation()
    {
        isInAnimation = true;
    }
}