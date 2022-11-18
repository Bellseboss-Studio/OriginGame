using UnityEngine;
using UnityEngine.Audio;

public class MusicService: MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot mute, menu, cityBuilding, blackJack, roulette;
    [SerializeField] private float timeToTransition;
    public void Configure(IMediatorAudioService audioService)
    {
        mute.TransitionTo(0.1f);
    }

    public void StayInMenu()
    {
        menu.TransitionTo(timeToTransition);
    }

    public void StayInCityBuilding()
    {
        cityBuilding.TransitionTo(timeToTransition);
    }

    public void StayInBlackJack()
    {
        blackJack.TransitionTo(timeToTransition);
    }

    public void StayInRoulette()
    {
        roulette.TransitionTo(timeToTransition);
    }
}