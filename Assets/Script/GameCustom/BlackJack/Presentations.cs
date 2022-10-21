using System.Collections;
using UnityEngine;

public class Presentations : MonoBehaviour
{
    [SerializeField] private Presentation enemy, player, game;
    private bool _finishEnemyPresentation;
    private bool _finishPlayerPresentation;
    private bool _finishPresentation;
    private bool _finishPresentationWinner = true;

    public bool FinishEnemyPresentation
    {
        get
        {
            var alter = _finishEnemyPresentation;
            if (_finishEnemyPresentation) _finishEnemyPresentation = false;
            return alter;
        }
    }
    public bool FinishPlayerPresentation
    {
        get
        {
            var alter = _finishPlayerPresentation;
            if (_finishPlayerPresentation) _finishPlayerPresentation = false;
            return alter;
        }
    }

    public bool FinishPresentation
    {
        get
        {
            var alter = _finishPresentation;
            if (_finishPresentation) _finishPresentation = false;
            return alter;
        }
    }

    public bool FinishPresentationOfWinner {
        get
        {
            var alter = _finishPresentationWinner;
            if (_finishPresentationWinner) _finishPresentationWinner = false;
            return alter;
        }
    }

    public void StartPresentationEnemy()
    {
        enemy.StartPresentation();
        StartCoroutine(WaitSecond());
    }

    private IEnumerator WaitSecond()
    {
        Debug.Log($"WaitSecond");
        while (!enemy.IsFinishPresentation)
        {
            yield return new WaitForSeconds(.2f);
        }
        _finishEnemyPresentation = true;
        Debug.Log($"WaitSecond");
    }
    private IEnumerator WaitSecondPlayer()
    {
        Debug.Log($"WaitSecondPlayer");
        while (!player.IsFinishPresentation)
        {
            yield return new WaitForSeconds(.2f);
        }
        _finishPlayerPresentation = true;
        Debug.Log($"WaitSecondPlayer");
    }
    private IEnumerator WaitForFinishPresentation()
    {
        Debug.Log($"WaitForFinishPresentation");
        while (!game.IsFinishPresentation)
        {
            yield return new WaitForSeconds(.2f);
        }
        _finishPresentation = true;
        Debug.Log($"WaitForFinishPresentation");
    }

    public void StartPresentationPlayer()
    {
        player.StartPresentation();
        StartCoroutine(WaitSecondPlayer());
    }

    public void StartPreparingGame()
    {
        game.StartPresentation();
        StartCoroutine(WaitForFinishPresentation());
    }

    public void WinGameAnimation()
    {
        
    }

    public void LoseGameAnimation()
    {
        
    }
}