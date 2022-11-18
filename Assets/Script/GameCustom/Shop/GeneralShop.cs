using System;
using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralShop : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private ElementInShopView prefabItem;
    [SerializeField] private int sceneCityBuilding;
    [SerializeField] private TextMeshProUGUI healthText, damageText, goldText;
    private int baseHeight = -200;
    private int baseStep = -400;
    private float left = 37f;
    private float right = 37f;
    private float height = 300f;
    void Start()
    {
        ServiceLocator.Instance.GetService<IAudioService>().StayInMenu();
        var sizeOfContainer = 0;
        var deltaHeight = baseHeight;
        var statsToShop = ServiceLocator.Instance.GetService<IShopService>().GetElements();
        foreach (var elementInShop in statsToShop)
        {
            var item = Instantiate(prefabItem, content.transform, true);
            item.Configure(elementInShop);
            item.GetRectTransform.anchorMin = new Vector2(0, 1);
            item.GetRectTransform.anchorMax = new Vector2(1, 1);
            Vector2 temp = new Vector2(left, deltaHeight-(height/2f));
            item.GetRectTransform.offsetMin = temp;
            temp = new Vector3(-right, temp.y+height);
            item.GetRectTransform.offsetMax = temp;
            item.OnUpdateData += () =>
            {
                OnUpdateDamage(ServiceLocator.Instance.GetService<IStatsInformation>().GetDamage());
                OnUpdateHealth(ServiceLocator.Instance.GetService<IStatsInformation>().GetHealth());
                OnUpdateGold(ServiceLocator.Instance.GetService<IGlobalInformation>().GetGold());
            };
            item.gameObject.transform.localScale = Vector3.one;
            deltaHeight += baseStep;
            sizeOfContainer++;   
        }

        content.sizeDelta = new Vector2(content.sizeDelta.x, 400 * sizeOfContainer);
        OnUpdateDamage(ServiceLocator.Instance.GetService<IStatsInformation>().GetDamage());
        OnUpdateHealth(ServiceLocator.Instance.GetService<IStatsInformation>().GetHealth());
        OnUpdateGold(ServiceLocator.Instance.GetService<IGlobalInformation>().GetGold());
        
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() =>
        {
            //Ignore
        });
    }

    private void OnUpdateGold(int gold)
    {
        goldText.text = $"{gold}";
    }

    private void OnUpdateHealth(int health)
    {
        healthText.text = $"{health}";
    }

    private void OnUpdateDamage(int damage)
    {
        damageText.text = $"{damage}";
    }

    public void GoToCityBuilding()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(sceneCityBuilding);
        });
    }
}