using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using UnityEngine;

public class ShowMessagesInScream : MonoBehaviour
{
    [SerializeField] private GeneralBlackJack messageGBJ;
    
    public void ShowWhoIsThere()
    {
        messageGBJ.ShowMessage("He's the enemy", "you'll win if his life drops to zero");
    }

    public void ShowWhenPowerHave()
    {
        messageGBJ.ShowMessage("The enemy", $" has {ServiceLocator.Instance.GetService<IStatsInformation>().GetEnemyDamage()} of damage");
    }

    public void ShowWhenHealthHave()
    {
        messageGBJ.ShowMessage("The enemy", $"has {ServiceLocator.Instance.GetService<IStatsInformation>().GetEnemyHealth()} life points");
    }
}