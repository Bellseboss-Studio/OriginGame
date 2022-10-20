using System;

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
    }
}