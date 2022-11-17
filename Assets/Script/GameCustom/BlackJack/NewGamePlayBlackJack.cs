using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewGamePlayBlackJack : MonoBehaviour
{
    [SerializeField] private Slider hpPlayer, hpEnemy;
    [SerializeField] private TextMeshProUGUI hpPlayerText, hpEnemyText;
    [SerializeField] private Renderer hpPlayerShader, hpEnemyShader;
    [SerializeField] private ControlWithCountOfLoads controllerOfLoadsPlayer, controllerOfLoadsEnemy;
    private int _hpToPlayer;
    private int _hpToEnemy;
    private static readonly int VidaAmount = Shader.PropertyToID("Vida_Amount");

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
        var hpPlayerValue = (float) newValue / _hpToPlayer;
        hpPlayer.value = hpPlayerValue;
        Debug.Log($"paso {hpPlayerValue}");
        Debug.Log($"exist Vida_Amount? {hpPlayerShader.material.HasFloat("Vida_Amount")}");
        Debug.Log($"into shader {hpPlayerShader.material.GetFloat("Vida_Amount")}");
        hpPlayerShader.material.SetFloat("Vida_Amount", hpPlayerValue);
        Debug.Log($"into shader after {hpPlayerShader.material.GetFloat("Vida_Amount")}");
        
    }
    
    public void UpdateHpEnemy(int newValue)
    {
        hpEnemyText.text = $"HP: {newValue}";
        var hpEnemyValue = (float) newValue / _hpToEnemy;
        hpEnemy.value = hpEnemyValue;
        Debug.Log($"paso {hpEnemyValue}");
        Debug.Log($"exist Vida_Amount? {hpEnemyShader.material.HasFloat("Vida_Amount")}");
        Debug.Log($"into shader {hpEnemyShader.material.GetFloat("Vida_Amount")}");
        hpEnemyShader.material.SetFloat("Vida_Amount", hpEnemyValue);
        Debug.Log($"into shader after {hpEnemyShader.material.GetFloat("Vida_Amount")}");
    }

    public void AddLoadPlayer()
    {
        controllerOfLoadsPlayer.AddLoad();
    }
    public void AddLoadEnemy()
    {
        controllerOfLoadsEnemy.AddLoad();
    }

    public void ResetLoadsPlayer()
    {
        controllerOfLoadsPlayer.ResetAll();
    }
    public void ResetLoadsEnemy()
    {
        controllerOfLoadsEnemy.ResetAll();
    }

    public void SetFromPlayer()
    {
        controllerOfLoadsPlayer.Set();
    }
    public void SetFromEnemy()
    {
        controllerOfLoadsEnemy.Set();
    }
}