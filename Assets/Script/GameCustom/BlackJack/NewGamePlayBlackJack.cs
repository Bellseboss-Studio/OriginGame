using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewGamePlayBlackJack : MonoBehaviour
{
    [SerializeField] private Slider hpPlayer, hpEnemy;
    [SerializeField] private TextMeshProUGUI hpPlayerText, hpEnemyText;
    private int _hpToPlayer;
    private int _hpToEnemy;

    public void Configure(int hpToPlayer, int hpToEnemy)
    {
        _hpToEnemy = hpToEnemy;
        _hpToPlayer = hpToPlayer;

        UpdateHpEnemy(_hpToEnemy);
        UpdateHpPlayer(_hpToPlayer);
    }

    public void UpdateHpPlayer(int newValue)
    {
        hpPlayerText.text = $"HP: {newValue}";
        hpPlayer.value = (float) newValue / _hpToPlayer;
    }
    
    public void UpdateHpEnemy(int newValue)
    {
        hpEnemyText.text = $"HP: {newValue}";
        hpEnemy.value = (float) newValue / _hpToEnemy;
    }
}