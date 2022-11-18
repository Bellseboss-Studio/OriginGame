using System;
using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using Terrains;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CityBuildingGeneral : MonoBehaviour
{
    [SerializeField] private CreateTerrainMap map;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private TextMeshProUGUI goldText, healthText, damageText;
    [SerializeField] private int sceneRoulette;
    [SerializeField] private int sceneShop;
    [SerializeField] private int damageInitial;
    [SerializeField] private int healthInitial;

    void Start()
    {
        map.CreateMap();
        var target = map.GetCenter();
        cameraController.SetTarget(target.gameObject);
        ServiceLocator.Instance.GetService<IStatsInformation>().SetCenter(target.GetHexagon());
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() => { });
        OnUpdateGold(ServiceLocator.Instance.GetService<IGlobalInformation>().GetGold());

        if (!ServiceLocator.Instance.GetService<IGlobalInformation>().IsFirstTimeInCityBuilding())
        {
            ServiceLocator.Instance.GetService<IGlobalInformation>().SetDamage(damageInitial);
            ServiceLocator.Instance.GetService<IGlobalInformation>().SetHealth(healthInitial);
            ServiceLocator.Instance.GetService<ILoadScene>().ShowMessageWithOneButton("What happen here?",
                "You will have to conquer all the territory for the family tradition, now you are weak. Try to go to Roulette and try your luck. \nFor the Horde",
                "Go to Roulette",
                GoToRoulette, () => { });
        }
        healthText.text = $"HP: {ServiceLocator.Instance.GetService<IStatsInformation>().GetHealth()}";
        damageText.text = $"PW: {ServiceLocator.Instance.GetService<IStatsInformation>().GetDamage()}";
        ServiceLocator.Instance.GetService<IAudioService>().StayInCityBuilding();
    }

    private void OnEnable()
    {
        ServiceLocator.Instance.GetService<IGlobalInformation>().OnUpdateGold += OnUpdateGold;
    }

    private void OnDisable()
    {
        ServiceLocator.Instance.GetService<IGlobalInformation>().OnUpdateGold -= OnUpdateGold;
    }

    private void OnUpdateGold(int gold)
    {
        goldText.text = $"Gold {gold}";
    }

    public void GoToRoulette()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(sceneRoulette);
        });
    }

    public void GoToShop()
    {
        
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(sceneShop);
        });
    }
}