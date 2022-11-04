using System;
using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RouletteGeneral : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nick, gold;
    [SerializeField] private GiveGoldForFirstTime isFirstTimeToPlay;
    [SerializeField] private int sceneCityBuilding;

    void Start()
    {
        nick.text = ServiceLocator.Instance.GetService<IGlobalInformation>().GetNickName();
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() => { });
        if (!ServiceLocator.Instance.GetService<IGlobalInformation>().IsWasPlayBefore())
        {
            ServiceLocator.Instance.GetService<ILoadScene>().Lock();
            isFirstTimeToPlay.GiveTheMoney();
        }
        UpdateGold(ServiceLocator.Instance.GetService<IGlobalInformation>().GetGold());
    }

    private void OnEnable()
    {
        ServiceLocator.Instance.GetService<IGlobalInformation>().OnUpdateGold += UpdateGold;
    }

    private void OnDisable()
    {
        ServiceLocator.Instance.GetService<IGlobalInformation>().OnUpdateGold -= UpdateGold;
    }

    private void UpdateGold(int goldSaved)
    {
        gold.text = $"Gold: {goldSaved}";
    }

    public void GoToCityBuilding()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(sceneCityBuilding);
        });
    }
}