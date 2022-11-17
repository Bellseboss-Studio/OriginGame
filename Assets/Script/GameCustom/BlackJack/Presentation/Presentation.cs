using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presentation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool _finishPresentation = true;
    private static readonly int Start = Animator.StringToHash("start");

    public bool IsFinishPresentation => _finishPresentation;

    public void StartPresentation()
    {
        _finishPresentation = false;
        animator.SetTrigger(Start);
    }

    public void FinishPresentation()
    {
        _finishPresentation = true;
    }
}