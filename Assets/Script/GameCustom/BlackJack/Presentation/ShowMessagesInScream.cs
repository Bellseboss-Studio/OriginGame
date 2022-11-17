using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using UnityEngine;

public class ShowMessagesInScream : MonoBehaviour
{
    [SerializeField] private GeneralBlackJack messageGBJ;
    
    public void ShowWhoIsThere()
    {
        messageGBJ.ShowMessage("He is the enemy", "You must make him run out of life");
    }

    public void ShowWhenPowerHave()
    {
        messageGBJ.ShowMessage("The enemy", $"have {ServiceLocator.Instance.GetService<IStatsInformation>().GetEnemyDamage()} of power");
    }

    public void ShowWhenHealthHave()
    {
        messageGBJ.ShowMessage("The enemy", $"have {ServiceLocator.Instance.GetService<IStatsInformation>().GetEnemyHealth()} of health");
    }
}