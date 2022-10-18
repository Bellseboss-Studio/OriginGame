using System;
using UnityEngine;

public class AwardController : MonoBehaviour
{
    [SerializeField] private Animator animatorAwards;
    private static readonly int Open = Animator.StringToHash("open");
    public Action OnFinishPresentationAwards;

    public void ShowAwards()
    {
        animatorAwards.SetBool(Open, true);
    }

    public void HideAwards()
    {
        animatorAwards.SetBool(Open, false);
    }

    public void FinishHideAwards()
    {
        OnFinishPresentationAwards?.Invoke();
    }
}
