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
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private int sceneRoulette;

    void Start()
    {
        map.CreateMap();
        var target = map.GetCenter();
        cameraController.SetTarget(target.gameObject);
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() => { });
        OnUpdateGold(ServiceLocator.Instance.GetService<IGlobalInformation>().GetGold());
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
}