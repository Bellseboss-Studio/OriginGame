using SystemOfExtras;
using UnityEngine;

public class OpenLoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() =>
        {
            //Start scripts custom
        });   
    }
}
