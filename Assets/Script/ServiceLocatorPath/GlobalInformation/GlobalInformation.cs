using System;
using System.Collections.Generic;
using System.Linq;
using Hexagons;
using SystemOfExtras.SavedData;
using UnityEngine;

namespace SystemOfExtras.GlobalInformationPath
{
    public class GlobalInformation:IGlobalInformation
    {
        private HexagonTemplate _hexagonTemplate;

        private List<string> hexagonsWinsFromPlayer;

        public GlobalInformation()
        {
            hexagonsWinsFromPlayer = new List<string>();
            var hexagonSaved = ServiceLocator.Instance.GetService<ISaveData>().Get("hexagonsWins");
            if (hexagonSaved != "")
            {
                foreach (var hexag in hexagonSaved.Split(";"))
                {
                    hexagonsWinsFromPlayer.Add(hexag);
                }
            }
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

        public void HexagonToBet(HexagonTemplate hexagonTemplate)
        {
            _hexagonTemplate = hexagonTemplate;
        }

        public void WinHexagon()
        {
            hexagonsWinsFromPlayer.Add(_hexagonTemplate.Id);
            _hexagonTemplate.PlayerWinThisHexagon();
            _hexagonTemplate = null;
            var hexagonsWins = "";
            foreach (var hexagonTemplate in hexagonsWinsFromPlayer)
            {
                hexagonsWins += hexagonTemplate + ";";
            }
            Debug.Log(hexagonsWins);
            ServiceLocator.Instance.GetService<ISaveData>().Save("hexagonsWins", hexagonsWins);
        }

        public bool ThisHexagonIsWinToPlayer(string position)
        {
            return hexagonsWinsFromPlayer.Any(hexagonTemplate => hexagonTemplate.Equals(position));
        }

        public void SetDamage(int damage)
        {
            ServiceLocator.Instance.GetService<ISaveData>().Save("damage", damage.ToString());
        }

        public void SetHealth(int health)
        {
            ServiceLocator.Instance.GetService<ISaveData>().Save("health", health.ToString());
        }

        public void LoseHexagon()
        {
            _hexagonTemplate = null;
        }
    }
}