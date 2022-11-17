using System.Collections.Generic;
using UnityEngine;

public class ControlWithCountOfLoads : MonoBehaviour
{
    [SerializeField] private List<GameObject> listOfLoads;
    private int _countOfLoads;
    private bool _setGame;

    public void AddLoad()
    {
        if (_countOfLoads >= listOfLoads.Count) return;
        listOfLoads[_countOfLoads].SetActive(true);
        if (_setGame)
        {
            listOfLoads[_countOfLoads].GetComponent<ParticleSystem>().enableEmission = true;
        }
        _countOfLoads++;
    }

    public void ResetAll()
    {
        foreach (var loads in listOfLoads)
        {
            loads.GetComponent<ParticleSystem>().enableEmission = false;
            loads.SetActive(false);
        }
        _countOfLoads = 0;
    }

    public void Set()
    {
        foreach (var load in listOfLoads)
        {
            load.GetComponent<ParticleSystem>().enableEmission = true;
            /*var emissionModule = load.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = true;*/
        }
        _setGame = true;
    }
}