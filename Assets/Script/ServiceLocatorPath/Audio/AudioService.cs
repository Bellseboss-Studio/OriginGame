using System;
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
}