using System;
using System.Collections;
using UnityEngine;

public class LoadSceneService : MonoBehaviour, ILoadScene
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorControllerAnimations animationController;
    public void Open(Action action)
    {
        StartCoroutine(LoadScene(false, action));
    }
    
    public void Close(Action action)
    {
        StartCoroutine(LoadScene(true, action));
    }

    private IEnumerator LoadScene(bool isOpen,Action action)
    {
        animator.SetBool("open", isOpen);
        while (!animationController.IsInAnimation)
        {
            yield return new WaitForSeconds(.2f);
        }
        yield return new WaitForSeconds(1);
        action?.Invoke();
    }
}