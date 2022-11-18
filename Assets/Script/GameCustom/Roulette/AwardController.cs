using System;
using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using TMPro;
using UnityEngine;

public class AwardController : MonoBehaviour
{
    [SerializeField] private Animator animatorAwards;
    [SerializeField] private TextMeshProUGUI resultText;
    private static readonly int Open = Animator.StringToHash("open");
    public Action OnFinishPresentationAwards;

    public void ShowAccumulate(int ganado)
    {
        resultText.text = $"Win \n {ganado}";
        animatorAwards.SetBool(Open, true);
    }

    public void HideAwards()
    {
        animatorAwards.SetBool(Open, false);
    }

    public void FinishHideAwards()
    {
        OnFinishPresentationAwards?.Invoke();
    }

    public void ShowLoseGold()
    {
        animatorAwards.SetBool(Open, true);
        resultText.text = $"Lose the loot";
    }

    public void ShowWinLoot(int totalLootWin)
    {
        animatorAwards.SetBool(Open, true);
        resultText.text = $"You Win Total {totalLootWin}";
        ServiceLocator.Instance.GetService<IAudioService>().RouletteTakeLoot();
        ServiceLocator.Instance.GetService<IGlobalInformation>().ReceiveGold(totalLootWin);
    }
}
