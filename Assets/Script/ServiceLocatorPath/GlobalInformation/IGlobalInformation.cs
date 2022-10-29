using System;
using Hexagons;

namespace SystemOfExtras.GlobalInformationPath
{
    public interface IGlobalInformation
    {
        void SaveNickName(string nick);
        string GetNickName();

        void ReceiveGold(int gold);

        void SpendGold(int gold);

        int GetGold();
        bool IsWasPlayBefore();
        Action<int> OnUpdateGold { get; set; }
        int GetBet();
        void SetBet(int bet);
        bool IsAuthenticated();
        void HexagonToBet(HexagonTemplate hexagonTemplate);
        void LoseHexagon();
        void WinHexagon();
        bool ThisHexagonIsWinToPlayer(string position);
        void SetDamage(int damage);
        void SetHealth(int healthInitial);
        
        string Tweet();
        Action TweetAction();
    }
}