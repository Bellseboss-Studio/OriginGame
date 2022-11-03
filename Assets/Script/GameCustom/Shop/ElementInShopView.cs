using System;
using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElementInShopView : MonoBehaviour, IElement
{
    [SerializeField] private TextMeshProUGUI title, value, cost;
    [SerializeField] private RectTransform getRectTransform;
    public Action OnUpdateData;
    private ElementInShop _element;

    public RectTransform GetRectTransform => getRectTransform;

    public void Configure(ElementInShop element)
    {
        _element = element;
        title.text = element.name;
        value.text = element.value;
        cost.text = $"{element.cost} g";
    }

    public void GetTheItem()
    {
        try
        {
            ServiceLocator.Instance.GetService<IShopService>().Buy(_element);
        }
        catch (Exception e)
        {
            //no le alacanzo el oro
            ServiceLocator.Instance.GetService<ILoadScene>().ShowMessageWithTwoButton(
                "Gold is not enough",
                "If you want to buy, you can play roulette or tweet the game to win gold. What do you want to do?",
                "Play Roulette", () =>
                {
                    //TODO go to roulette
                    ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
                    {
                        SceneManager.LoadScene(ServiceLocator.Instance.GetService<IGlobalInformation>()
                            .GetSceneForRoulette());
                    });
                },
                "Tweet the game",
                () =>
                {
                    ServiceLocator.Instance.GetService<IGlobalInformation>().TweetAction().Invoke();
                    OnUpdateData?.Invoke();
                }, () =>
                {
                    //TODO whats happend if the cancel way
                });
        }
        OnUpdateData?.Invoke();
    }

    public ElementInShop GetElement()
    {
        return _element;
    }
}