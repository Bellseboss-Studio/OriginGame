using System;
using UnityEngine;
using UnityEngine.UI;

public class NewGamePlayBlackJack : MonoBehaviour
{
    [SerializeField] private Slider hpPlayer, hpEnemy;
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
        hpPlayer.value = newValue / _hpToPlayer;
    }
    
    public void UpdateHpEnemy(int newValue)
    {
        hpEnemy.value = newValue / _hpToEnemy;
    }
}