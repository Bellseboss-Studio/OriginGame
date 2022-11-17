using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using UnityEngine;

public class PlayerShowMessage : MonoBehaviour
{
    [SerializeField] private GeneralBlackJack messageGBJ;

    public void ShowWhenHealthYouHave()
    {
        messageGBJ.ShowMessage("You health", $"is {ServiceLocator.Instance.GetService<IStatsInformation>().GetHealth()}");
    }
    
    
    public void ShowWhenDamageYouHave()
    {
        messageGBJ.ShowMessage("You Damage", $"is {ServiceLocator.Instance.GetService<IStatsInformation>().GetDamage()}");
    }
}