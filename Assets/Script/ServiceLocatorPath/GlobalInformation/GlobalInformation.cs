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
            var totalGold = int.Parse(ServiceLocator.Instance.GetService<ISaveData>().Get("gold"));
            totalGold += gold;
            ServiceLocator.Instance.GetService<ISaveData>().Save("gold", totalGold.ToString());
            OnUpdateGold?.Invoke(totalGold);
        }

        public void SpendGold(int gold)
        {
            var totalGold = int.Parse(ServiceLocator.Instance.GetService<ISaveData>().Get("gold"));
            totalGold -= gold;
            ServiceLocator.Instance.GetService<ISaveData>().Save("gold", totalGold.ToString());
            OnUpdateGold?.Invoke(totalGold);
        }

        public int GetGold()
        {
            return int.Parse(ServiceLocator.Instance.GetService<ISaveData>().Get("gold")); 
        }

        public bool IsWasPlayBefore()
        {
            var isFirstTimeToPlay =
                bool.Parse(ServiceLocator.Instance.GetService<ISaveData>().Get("firstTimeToPlay"));
            if (!isFirstTimeToPlay)
            {
                ServiceLocator.Instance.GetService<ISaveData>().Save("firstTimeToPlay", true.ToString());
            }

            return isFirstTimeToPlay;
        }

        public Action<int> OnUpdateGold { get; set; }
        public int GetBet()
        {
            return int.Parse(ServiceLocator.Instance.GetService<ISaveData>().Get("bet"));
        }
        
        public void SetBet(int bet)
        {
            ServiceLocator.Instance.GetService<ISaveData>().Save("bet", bet.ToString());
        }
    }
}