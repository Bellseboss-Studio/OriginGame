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

        private string messateForTwetter;

        private Action TwetterActionCustom;

        public GlobalInformation()
        {
            messateForTwetter = "Estoy jugando a https://bellseboss.itch.io/origin-game es el mejor juego de todos @bellseboss #gamedev";
            hexagonsWinsFromPlayer = new List<string>();
            var hexagonSaved = ServiceLocator.Instance.GetService<ISaveData>().Get("hexagonsWins");
            if (hexagonSaved != "")
            {
                foreach (var hexag in hexagonSaved.Split(";"))
                {
                    hexagonsWinsFromPlayer.Add(hexag);
                }
            }

            TwetterActionCustom = TwetterAction;
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
            if (totalGold - gold < 0)
            {
                throw new Exception("Enough gold");
            }
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

        public string Tweet()
        {
            return messateForTwetter;
        }

        public Action TweetAction()
        {
            return TwetterActionCustom;
        }

        public void LoseHexagon()
        {
            _hexagonTemplate = null;
        }

        public void TwetterAction()
        {
            var message = ServiceLocator.Instance.GetService<IGlobalInformation>().Tweet();
            Application.OpenURL($"https://twitter.com/intent/tweet?text={message}");
            ServiceLocator.Instance.GetService<IGlobalInformation>().ReceiveGold(200);
        }
    }
}