using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using TMPro;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
    [SerializeField] private Animator animRoulette;
    [SerializeField] private AwardController award;
    [SerializeField] private TextMeshProUGUI textToAutoRun, resultBet;
    [SerializeField] private TMP_InputField betInput;
    private static readonly int Go = Animator.StringToHash("go");
    private bool _autoRun;
    private int _apuestaPuesta;
    private int _result;

    private void Start()
    {
        award.OnFinishPresentationAwards += OnFinishPresentationAwards;
        _apuestaPuesta = ServiceLocator.Instance.GetService<IGlobalInformation>().GetBet();
        betInput.text = _apuestaPuesta.ToString();
    }

    public void LessBet()
    {
        _apuestaPuesta -= 1;
        if (_apuestaPuesta <= 0)
        {
            _apuestaPuesta = 1;
        }
        ServiceLocator.Instance.GetService<IGlobalInformation>().SetBet(_apuestaPuesta);
        betInput.text = _apuestaPuesta.ToString();
    }

    public void MoreBet()
    {
        _apuestaPuesta += 1;
        ServiceLocator.Instance.GetService<IGlobalInformation>().SetBet(_apuestaPuesta);
        betInput.text = _apuestaPuesta.ToString();
    }

    private void OnFinishPresentationAwards()
    {
        if (_autoRun)
        {
            animRoulette.SetTrigger(Go);   
        }
    }

    public void RunRouletteWhitAutoRun()
    {
        if (_autoRun)
        {
            StopAutoRun();
            textToAutoRun.text = "Auto Play";
            return;
        }
        _autoRun = true;
        Roulette();
        textToAutoRun.text = "Stop Auto";
    }

    public void Roulette()
    {
        ServiceLocator.Instance.GetService<IGlobalInformation>().SpendGold(_apuestaPuesta);
        animRoulette.SetTrigger(Go);
    }

    public void SetResult()
    {
        _result = 0;
        _result = ServiceLocator.Instance.GetService<IRouletteService>().GetResult();
        resultBet.text = $"X {_result}";
    }

    public void AwardPresentation()
    {
        //Presentacion de premio?
        award.ShowAwards(_apuestaPuesta * _result);
    }
    
    public void StopAutoRun()
    {
        _autoRun = false;
    }
}
