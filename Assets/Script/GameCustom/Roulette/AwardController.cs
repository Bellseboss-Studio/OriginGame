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
    private int _ganado;

    public void ShowAwards(int ganado)
    {
        _ganado = ganado;
        resultText.text = $"Win \n {ganado}";
        animatorAwards.SetBool(Open, true);
    }

    public void HideAwards()
    {
        ServiceLocator.Instance.GetService<IGlobalInformation>().ReceiveGold(_ganado);
        animatorAwards.SetBool(Open, false);
    }

    public void FinishHideAwards()
    {
        OnFinishPresentationAwards?.Invoke();
    }
}
