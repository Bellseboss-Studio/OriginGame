using System;
using SystemOfExtras.SavedData;

namespace SystemOfExtras.GlobalInformationPath
{
    public class GlobalInformation:IGlobalInformation
    {
        public GlobalInformation()
        {
        }

        public void SaveNickName(string nick)
        {
            ServiceLocator.Instance.GetService<ISaveData>().Save("nick", nick);
        }

        public string GetNickName()
        {
            return ServiceLocator.Instance.GetService<ISaveData>().Get("nick");
        }

        public void ReceiveGold(int gold)
        {
            int.TryParse(ServiceLocator.Instance.GetService<ISaveData>().Get("gold"), out var totalGold);
            totalGold += gold;
            ServiceLocator.Instance.GetService<ISaveData>().Save("gold", totalGold.ToString());
            OnUpdateGold?.Invoke(totalGold);
        }

        public void SpendGold(int gold)
        {
            int.TryParse(ServiceLocator.Instance.GetService<ISaveData>().Get("gold"), out var totalGold);
            totalGold -= gold;
            ServiceLocator.Instance.GetService<ISaveData>().Save("gold", totalGold.ToString());
            OnUpdateGold?.Invoke(totalGold);
        }

        public int GetGold()
        {
            return int.TryParse(ServiceLocator.Instance.GetService<ISaveData>().Get("gold"), out var gold) ? gold : 0;
        }

        public bool IsWasPlayBefore()
        {
            bool.TryParse(ServiceLocator.Instance.GetService<ISaveData>().Get("firstTimeToPlay"),
                out var isFirstTimeToPlay);
            if (!isFirstTimeToPlay)
            {
                ServiceLocator.Instance.GetService<ISaveData>().Save("firstTimeToPlay", true.ToString());
            }
            return isFirstTimeToPlay;
        }

        public Action<int> OnUpdateGold { get; set; }
        public int GetBet()
        {
            return int.TryParse(ServiceLocator.Instance.GetService<ISaveData>().Get("bet"), out var bet) ? bet : 0;
        }
        
        public void SetBet(int bet)
        {
            ServiceLocator.Instance.GetService<ISaveData>().Save("bet", bet.ToString());
        }

        public bool IsAuthenticated()
        {
            return GetNickName() != "";
        }
    }
}