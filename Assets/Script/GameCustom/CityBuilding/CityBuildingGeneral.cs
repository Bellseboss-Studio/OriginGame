using SystemOfExtras;
using Terrains;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CityBuildingGeneral : MonoBehaviour
{
    [SerializeField] private CreateTerrainMap map;
    [SerializeField] private CameraController cameraController;
    void Start()
    {
        map.CreateMap();
        var target = map.GetCenter();
        cameraController.SetTarget(target.gameObject);
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() => { });
    }

    public void GoToRoulette()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(2);
        });
    }
}