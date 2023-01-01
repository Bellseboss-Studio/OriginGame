using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SfxService: MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private List<AudioClip> clips;
    private Dictionary<string, AudioClip> _clipsInOrdered;
    private IMediatorAudioService _audioService;

    public void Configure(IMediatorAudioService audioService)
    {
        _audioService = audioService;
        _clipsInOrdered = new Dictionary<string, AudioClip>();
        foreach (var clip in clips)
        {
            _clipsInOrdered.Add(clip.name,clip);
        }
    }

    private void PlayOneShot(string nameOfSfx)
    {
        source.PlayOneShot(_clipsInOrdered[nameOfSfx]);
        _audioService.MuteMusic();
    }

    public void ClickRandom()
    {
        var clicks = GetWithContainer("Click");
        var random = Random.Range(0, clicks.Count);
        PlayOneShot(clicks[random].Key);
    }

    public void DrawCard()
    {
        var card = GetWithContainer("Carta");
        var random = Random.Range(0, card.Count);
        PlayOneShot(card[random].Key);
    }

    public void PlayRoulette(int randomRound)
    {
        var roulette = GetWithContainer($"Ruleta_{randomRound}");
        var random = Random.Range(0, roulette.Count);
        Debug.Log($"{roulette.Count} - {random}");
        PlayOneShot(roulette[random].Key);
    }

    private List<KeyValuePair<string, AudioClip>> GetWithContainer(string name)
    {
        return _clipsInOrdered.Where(audioClip => audioClip.Key.Contains(name)).ToList();
    }

    public void RouletteTakeLoot()
    {
        PlayOneShot(GetWithContainer("SFX_Ruleta_Win-Origin")[0].Key);
    }

    public void RouletteWin()
    {
        PlayOneShot(GetWithContainer("SFX_BJ_Win-Origin")[0].Key);
    }

    public void RouletteLose()
    {
        PlayOneShot(GetWithContainer("SFX_BJ_Lose-Origin")[0].Key);
    }

    public void TakeFirstTokens()
    {
        PlayOneShot(GetWithContainer("SFX_InicialPack2_Vr2-Origin")[0].Key);
    }

    public void Transition()
    {
        var transition = GetWithContainer($"Transicion");
        var random = Random.Range(0, transition.Count);
        Debug.Log($"{transition.Count} - {random}");
        PlayOneShot(transition[random].Key);        
    }

    public void BlackJackWin()
    {
        RouletteWin();
    }

    public void BlackJackLose()
    {
        RouletteLose();
    }
}