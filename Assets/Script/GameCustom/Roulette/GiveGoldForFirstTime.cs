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
    [SerializeField] private int goldInitial;

    private void Start()
    {
        buttonToFinish.onClick.AddListener(() =>
        {
            animatorToReceiveGold.SetTrigger(Finish);
        });
    }

    public void GiveTheToken()
    {
        animatorToReceiveGold.SetTrigger(StartAnim);
        StartCoroutine(GiveToken());
    }

    private IEnumerator GiveToken()
    {
        yield return new WaitForSeconds(2);
        ServiceLocator.Instance.GetService<IGlobalInformation>().ReceiveToken(goldInitial);
        ServiceLocator.Instance.GetService<ILoadScene>().Unlock();
    }
}