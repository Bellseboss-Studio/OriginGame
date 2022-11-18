using UnityEngine;

public class AudioService : MonoBehaviour, IAudioService, IMediatorAudioService
{
    [SerializeField] private MusicService musicService;
    [SerializeField] private SfxService sfxService;

    private void Start()
    {
        musicService.Configure(this);
        sfxService.Configure(this);
    }

    public void StayInMenu()
    {
        musicService.StayInMenu();
    }

    public void StayInCityBuilding()
    {
        musicService.StayInCityBuilding();
    }

    public void StayInRoulette()
    {
        musicService.StayInRoulette();
    }

    public void StayInBlackJack()
    {
        musicService.StayInBlackJack();
    }

    public void Click()
    {
        sfxService.ClickRandom();
    }

    public void DrawCard()
    {
        sfxService.DrawCard();
    }

    public void PlayRoulette(int randomRound)
    {
        sfxService.PlayRoulette(randomRound);
    }

    public void RouletteLose()
    {
        sfxService.RouletteLose();
    }

    public void RouletteWin()
    {
        sfxService.RouletteWin();
    }

    public void RouletteTakeLoot()
    {
        sfxService.RouletteTakeLoot();
    }

    public void TakeFirstTokens()
    {
        sfxService.TakeFirstTokens();
    }

    public void Transition()
    {
        sfxService.Transition();
    }

    public void BlackJackWin()
    {
        sfxService.BlackJackWin();
    }

    public void BlackJackLose()
    {
        sfxService.BlackJackLose();   
    }
}