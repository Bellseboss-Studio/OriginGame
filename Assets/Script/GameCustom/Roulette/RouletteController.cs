using System;
using TMPro;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
    [SerializeField] private Animator animRoulette;
    [SerializeField] private AwardController award;
    [SerializeField] private TextMeshProUGUI textToAutoRun;
    private static readonly int Go = Animator.StringToHash("go");
    private bool autoRun;

    private void Start()
    {
        award.OnFinishPresentationAwards += OnFinishPresentationAwards;
    }

    private void OnFinishPresentationAwards()
    {
        if (autoRun)
        {
            animRoulette.SetTrigger(Go);   
        }
    }

    public void RunRouletteWhitAutoRun()
    {
        if (autoRun)
        {
            StopAutoRun();
            textToAutoRun.text = "Auto Play";
            return;
        }
        autoRun = true;
        Roulette();
        textToAutoRun.text = "Stop Auto";
    }

    public void Roulette()
    {
        animRoulette.SetTrigger(Go);
    }

    public void AwardPresentation()
    {
        //Presentacion de premio?
        award.ShowAwards();
    }
    
    public void StopAutoRun()
    {
        autoRun = false;
    }
}
