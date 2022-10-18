using System.Collections;
using System.Collections.Generic;
using SystemOfExtras;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RouletteGeneral : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() => { });
    }

    public void GoToCityBuilding()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(3);
        });
    }
}
