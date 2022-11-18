using System;
using System.Collections;
using SystemOfExtras;
using UnityEngine;

public class LoadSceneService : MonoBehaviour, ILoadScene, ILoadScenePrivate
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorControllerAnimations animationController;
    [SerializeField] private GameObject panelLocked;
    [SerializeField] private MessageWithTwoButtons twoButtons;
    [SerializeField] private MessageWithOneButtons oneButtons;
    private static readonly int Closekey = Animator.StringToHash("close");
    private static readonly int Buttons = Animator.StringToHash("buttons");

    private void Start()
    {
        Unlock();
        twoButtons.Configure(this);
        oneButtons.Configure(this);
    }

    public void Open(Action action)
    {
        ServiceLocator.Instance.GetService<IAudioService>().Transition();
        StartCoroutine(LoadScene(false, action));
        Unlock();
    }
    
    public void Close(Action action)
    {
        ServiceLocator.Instance.GetService<IAudioService>().Transition();
        StartCoroutine(LoadScene(true, action));
    }

    public void Lock()
    {
        panelLocked.SetActive(true);
    }

    public void Unlock()
    {
        panelLocked.SetActive(false);
    }

    public void ShowMessageWithTwoButton(string title, string message, string titlebuttonone, Action actionButtonOne,
        string titlebuttontwo, Action actionButtonTwo, Action actionToCancel)
    {
    ServiceLocator.Instance.GetService<IAudioService>().Transition();
        animator.SetInteger("buttons", 2);
        twoButtons.Show(title, message, titlebuttonone, actionButtonOne, titlebuttontwo, actionButtonTwo,
            actionToCancel);
    }

    public void ShowMessageWithOneButton(string title, string message, string titleOneButton, Action actionButtonOne,
        Action actionToCancel)
    {
        ServiceLocator.Instance.GetService<IAudioService>().Transition();
        animator.SetInteger("buttons", 1);
        oneButtons.Show(title, message, titleOneButton, actionButtonOne, actionToCancel);
    }

    private IEnumerator LoadScene(bool isOpen,Action action)
    {
        animator.SetBool("open", isOpen);
        yield return new WaitForSeconds(.1f);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        action?.Invoke();
    }

    public void CloseMessages()
    {
        //TODO close message to animation mode
        animator.SetInteger(Buttons, 0);
        animator.SetTrigger(Closekey);
        ServiceLocator.Instance.GetService<IAudioService>().Transition();
    }
}