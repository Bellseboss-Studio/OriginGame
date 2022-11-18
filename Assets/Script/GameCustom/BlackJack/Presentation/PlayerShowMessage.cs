using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using UnityEngine;

public class PlayerShowMessage : MonoBehaviour
{
    [SerializeField] private ShowMessageTransitionally messageGBJ;

    public void ShowWhenHealthYouHave()
    {
        messageGBJ.ShowMessage("Your health", $"is {ServiceLocator.Instance.GetService<IStatsInformation>().GetHealth()}");
    }
    
    
    public void ShowWhenDamageYouHave()
    {
        messageGBJ.ShowMessage("Your Damage", $"is {ServiceLocator.Instance.GetService<IStatsInformation>().GetDamage()}");
    }
}