using System;
using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RouletteGeneral : MonoBehaviour, IRouletteGeneral
{
    [SerializeField] private TextMeshProUGUI nick, gold, token;
    [SerializeField] private GiveGoldForFirstTime isFirstTimeToPlay;
    [SerializeField] private int sceneCityBuilding;
    [SerializeField] private RouletteController roulete;

    void Start()
    {
        nick.text = ServiceLocator.Instance.GetService<IGlobalInformation>().GetNickName();
        var tokenVar = ServiceLocator.Instance.GetService<IGlobalInformation>().GetTokens();
        token.text = $"Tokens: {tokenVar}";
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() => { });
        if (!ServiceLocator.Instance.GetService<IGlobalInformation>().IsWasPlayBefore())
        {
            ServiceLocator.Instance.GetService<ILoadScene>().Lock();
            isFirstTimeToPlay.GiveTheToken();
        }
        UpdateGold(ServiceLocator.Instance.GetService<IGlobalInformation>().GetGold());
        roulete.Configure(this);
    }

    private void OnEnable()
    {
        ServiceLocator.Instance.GetService<IGlobalInformation>().OnUpdateGold += UpdateGold;
        ServiceLocator.Instance.GetService<IGlobalInformation>().OnUpdateToken += UpdateToken;
    }

    private void OnDisable()
    {
        ServiceLocator.Instance.GetService<IGlobalInformation>().OnUpdateGold -= UpdateGold;
        ServiceLocator.Instance.GetService<IGlobalInformation>().OnUpdateToken -= UpdateToken;
    }

    private void UpdateGold(int goldSaved)
    {
        gold.text = $"Gold: {goldSaved}";
    }
    private void UpdateToken(int tokenSaved)
    {
        token.text = $"Tokens: {tokenSaved}";
    }

    public void GoToCityBuilding()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(sceneCityBuilding);
        });
    }
}