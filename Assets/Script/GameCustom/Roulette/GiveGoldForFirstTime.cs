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
    [SerializeField] private int damageInitial;
    [SerializeField] private int healthInitial;

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
        ServiceLocator.Instance.GetService<IGlobalInformation>().ReceiveGold(goldInitial);
        ServiceLocator.Instance.GetService<IGlobalInformation>().SetDamage(damageInitial);
        ServiceLocator.Instance.GetService<IGlobalInformation>().SetHealth(healthInitial);
        ServiceLocator.Instance.GetService<ILoadScene>().Unlock();
    }
}