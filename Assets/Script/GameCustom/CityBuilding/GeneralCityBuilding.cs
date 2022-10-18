using System.Collections;
using System.Collections.Generic;
using SystemOfExtras;
using Terrains;
using UnityEngine;

public class GeneralCityBuilding : MonoBehaviour
{
    [SerializeField] private CreateTerrainMap map;
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
}
