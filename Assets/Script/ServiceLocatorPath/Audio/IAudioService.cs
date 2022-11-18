public interface IAudioService
{
    void StayInMenu();
    void StayInCityBuilding();
    void StayInRoulette();
    void StayInBlackJack();
    void Click();
    void DrawCard();
    void PlayRoulette(int randomRound);
    void RouletteLose();
    void RouletteWin();
    void RouletteTakeLoot();
    void TakeFirstTokens();
    void Transition();
    void BlackJackWin();
    void BlackJackLose();
}