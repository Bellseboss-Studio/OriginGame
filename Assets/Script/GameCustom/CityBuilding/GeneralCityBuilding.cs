using SystemOfExtras;
using Terrains;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralCityBuilding : MonoBehaviour
{
    [SerializeField] private CreateTerrainMap map;
    [SerializeField] private int sceneRulete;
    // Start is called before the first frame update
    void Start()
    {
        //TODO start all scripts
        var h = map.Hexagons.GetLength(0) / 2;
        var w = map.Hexagons.GetLength(1) / 2;
        var newTarget = map.Hexagons[h, w];
        ServiceLocator.Instance.GetService<ICameraController>().SetTarget(newTarget.gameObject);
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() => { });
    }

    public void GoToRulete()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(sceneRulete);
        });
    }
}