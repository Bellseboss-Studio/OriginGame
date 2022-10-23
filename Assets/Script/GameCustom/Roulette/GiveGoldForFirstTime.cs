using System;
using System.Collections;
using System.Collections.Generic;
using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using UnityEngine;
using UnityEngine.UI;

public class GiveGoldForFirstTime : MonoBehaviour
{
    [SerializeField] private Animator animatorToReceiveGold;
    [SerializeField] private Button buttonToFinish;
    private static readonly int StartAnim = Animator.StringToHash("start");
    private static readonly int Finish = Animator.StringToHash("finish");
    [SerializeField] private int goldToReceive;

    private void Start()
    {
        buttonToFinish.onClick.AddListener(() =>
        {
            animatorToReceiveGold.SetTrigger(Finish);
        });
    }

    public void GiveTheMoney()
    {
        animatorToReceiveGold.SetTrigger(StartAnim);
        StartCoroutine(GiveGold());
    }

    private IEnumerator GiveGold()
    {
        yield return new WaitForSeconds(2);
        ServiceLocator.Instance.GetService<IGlobalInformation>().ReceiveGold(goldToReceive);
        ServiceLocator.Instance.GetService<ILoadScene>().Unlock();
    }
}