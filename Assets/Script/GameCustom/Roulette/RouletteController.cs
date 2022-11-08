using System;
using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RouletteController : MonoBehaviour
{
    [SerializeField] private Animator animRoulette;
    [SerializeField] private AwardController award;
    [SerializeField] private TextMeshProUGUI textToAutoRun, resultBet, lootAccumulate;
    [SerializeField] private TMP_InputField betInput;
    [SerializeField] private int spendForThrowOfRoulette, multipleOfThrow;
    [SerializeField] private Image skullResult;
    private static readonly int Go = Animator.StringToHash("go");
    private bool _autoRun;
    private int _result;
    private int loot;
    private IRouletteGeneral _rouletteGeneral;

    private void Start()
    {
        resultBet.gameObject.SetActive(true);
        skullResult.gameObject.SetActive(false);
        lootAccumulate.text = $"Loot: {loot}";
    }

    private void OnEnable()
    {
        award.OnFinishPresentationAwards += OnFinishPresentationAwards;
    }

    private void OnDisable()
    {
        award.OnFinishPresentationAwards -= OnFinishPresentationAwards;
    }

    private void OnFinishPresentationAwards()
    {
        if (_result == 0)
        {
            loot = 0;
            lootAccumulate.text = $"Loot: {loot}";
        }
        else
        {
            lootAccumulate.text = $"Loot: {loot}";
        }
        if (_autoRun)
        {
            Roulette();
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
        try
        {
            ServiceLocator.Instance.GetService<IGlobalInformation>().SpendTokens(spendForThrowOfRoulette * multipleOfThrow);
            //animRoulette.SetTrigger(Go);
            var randomRound = Random.Range(1, 12);
            var animationToPlayInRoulette = $"Round{randomRound}";
            Debug.Log(animationToPlayInRoulette);
            animRoulette.Play(animationToPlayInRoulette);
        }
        catch (Exception e)
        {
            
            //no le alacanzo el oro
            ServiceLocator.Instance.GetService<ILoadScene>().ShowMessageWithTwoButton(
                "Tokens is not enough", 
                "If you wanna to play, you can play roulette or tweet the game to win Tokens. What do you want to do?", 
                "Tweet the game",
                () =>
                {
                    var message = ServiceLocator.Instance.GetService<IGlobalInformation>().Tweet();
                    Application.OpenURL($"https://twitter.com/intent/tweet?text={message}");
                    ServiceLocator.Instance.GetService<IGlobalInformation>().ReceiveToken(20);
                },
                "Explore the map", () =>
                {
                    SceneManager.LoadScene(ServiceLocator.Instance.GetService<IGlobalInformation>()
                        .GetSceneForCityBuilding());
                },
                () =>
                {
                    //TODO whats happend if the cancel way
                });
        }
    }

    public void SetResult()
    {
        _result = 0;
        _result = ServiceLocator.Instance.GetService<IRouletteService>().GetResult();
        if (_result == 0)
        {
            resultBet.gameObject.SetActive(false);
            skullResult.gameObject.SetActive(true);
            ServiceLocator.Instance.GetService<IRouletteService>().ResetResult();
        }
        else
        {
            resultBet.gameObject.SetActive(true);
            skullResult.gameObject.SetActive(false);
            resultBet.text = $"X {_result}";
        }
        
    }

    public void AwardPresentation()
    {
        if (_result == 0)
        {
            award.ShowLoseGold();
        }
        else
        {
            var betWin = _result;
            loot *= betWin;
            award.ShowAccumulate(betWin);
        }
    }
    
    public void StopAutoRun()
    {
        _autoRun = false;
    }

    public void TakeLoot()
    {
        award.ShowWinLoot(loot);
        loot = 0;
    }

    public void Configure(IRouletteGeneral rouletteGeneral)
    {
        _rouletteGeneral = rouletteGeneral;
    }
}
