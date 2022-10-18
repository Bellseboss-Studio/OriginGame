using System.Collections;
using System.Collections.Generic;
using SystemOfExtras;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralRulete : MonoBehaviour
{
    [SerializeField] private int sceneCityBuilding;
    public void GoToCityBuilding()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(sceneCityBuilding);
        });
    }
    
}
