using System;
using System.Collections.Generic;
using System.Linq;
using Hexagons;
using SystemOfExtras.SavedData;
using UnityEngine;

namespace SystemOfExtras.GlobalInformationPath
{
    public class GlobalInformation:IGlobalInformation, IStatsInformation, IShopService
    {

        public Action<int> OnUpdateGold { get; set; }
        public Action<int> OnUpdateToken { get; set; }
        public Action<int> OnUpdateHealth { get; set; }
        public Action<int> OnUpdateDamage { get; set; }
        
        private HexagonTemplate _hexagonTemplate;

        private List<string> hexagonsWinsFromPlayer;

        private Action TwetterActionCustom;

        private int healthBase = 100;
        private int damageBase = 10;

        private readonly List<ElementInShop> _elementsInShop;
        private readonly int _sceneCityBuilding;
        private readonly int _sceneRoulette;
        private readonly int _sceneShop;
        private readonly int _healthEnemyBase;
        private readonly int _damageEnemyBase;
        private Hexagon _center;
        private int _healthEnemy = 1, _damageEnemy = 1;
        private readonly float _increment;

        public GlobalInformation(int sceneCityBuilding,int sceneRoulette, int sceneShop, int healthEnemyBase, int damageEnemyBase, float increment)
        {
            _increment = increment;
            _sceneCityBuilding = sceneCityBuilding;
            _sceneRoulette = sceneRoulette;
            _sceneShop = sceneShop;
            _healthEnemyBase = healthEnemyBase;
            _damageEnemyBase = damageEnemyBase;
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
            
            _elementsInShop = new()
            {
                new ElementInShop
                {
                    key = "health",
                    name = "Health",
                    value = "10",
                    cost = 10
                },
                new ElementInShop
                {
                    key = "damage",
                    name = "Damage",
                    value = "1",
                    cost = 10
                }
            };
        }

        public void OnUpdateDataDamage(int obj)
        {
            Debug.Log($"damage obj {obj}");
            OnUpdateDamage.Invoke(obj);
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
            return "I'm playing https://bellseboss.itch.io/origin-game the best game ever created TY @bellseboss #gamedev";
        }

        public Action TweetAction()
        {
            return TwetterActionCustom;
        }

        public int GetTokens()
        {
            return int.TryParse(ServiceLocator.Instance.GetService<ISaveData>().Get("token"), out var token) ? token : 0;
        }
        public void SpendTokens(int tokenCount)
        {
            int.TryParse(ServiceLocator.Instance.GetService<ISaveData>().Get("token"), out var totalToken);
            if (totalToken - tokenCount < 0)
            {
                throw new Exception("Enough Tokens");
            }
            totalToken -= tokenCount;
            ServiceLocator.Instance.GetService<ISaveData>().Save("token", totalToken.ToString());
            OnUpdateToken?.Invoke(totalToken);
        }

        public void ReceiveToken(int tokenCount)
        {
            int.TryParse(ServiceLocator.Instance.GetService<ISaveData>().Get("token"), out var totalTokens);
            totalTokens += tokenCount;
            ServiceLocator.Instance.GetService<ISaveData>().Save("token", totalTokens.ToString());
            OnUpdateToken?.Invoke(totalTokens);            
        }

        public int GetSceneForRoulette()
        {
            return _sceneRoulette;
        }

        public int GetSceneForCityBuilding()
        {
            return _sceneCityBuilding;
        }

        public bool IsFirstTimeInCityBuilding()
        {
            bool.TryParse(ServiceLocator.Instance.GetService<ISaveData>().Get("firstTimeInCityBuilding"),
                out var isFirstTimeInCityBuilding);
            if (!isFirstTimeInCityBuilding)
            {
                ServiceLocator.Instance.GetService<ISaveData>().Save("firstTimeInCityBuilding", true.ToString());
            }
            return isFirstTimeInCityBuilding;
        }

        public Hexagon GetHexagonInBet()
        {
            if (_hexagonTemplate.GetHexagon() != null)
            {
                return _hexagonTemplate.GetHexagon();
            }
            throw new Exception("Hexagon not found");
        }

        public int GetHealth()
        {
            //TODO define of how to increment the health
            return int.TryParse(ServiceLocator.Instance.GetService<ISaveData>().Get("health"), out var health) ? health : 1;
        }

        public int GetDamage()
        {
            //TODO define of how to increment the damage
            return int.TryParse(ServiceLocator.Instance.GetService<ISaveData>().Get("damage"), out var damage) ? damage : 1;
        }

        public void CalculateStatsForEnemy(Hexagon hexagon)
        {
            var centerPositions = _center.Position.Split("-");
            int.TryParse(centerPositions[0], out var xCenter);
            int.TryParse(centerPositions[1], out var yCenter);

            var hexagonPositions = hexagon.Position.Split("-");
            int.TryParse(hexagonPositions[0], out var xHexagon);
            int.TryParse(hexagonPositions[1], out var yHexagon);

            var distance = Mathf.Abs(xCenter - xHexagon) + Mathf.Abs(yCenter - yHexagon);

            Debug.Log($"Distance: {distance}");
            //Increment of 10% for any distance
            _healthEnemy = _healthEnemyBase;
            _damageEnemy = _damageEnemyBase;
            for (int i = 0; i < distance; i++)
            {
                _healthEnemy += (int) Math.Round(_healthEnemy * _increment);
                _damageEnemy += (int) Math.Round(_damageEnemy * _increment);
            }

            Debug.Log($"Health: {_healthEnemy}");
            Debug.Log($"Damage: {_damageEnemy}");
        }

        public void SetCenter(Hexagon hexagon)
        {
            _center = hexagon;
        }

        public int GetEnemyHealth()
        {
            return _healthEnemy;
        }

        public int GetEnemyDamage()
        {
            return _damageEnemy;
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

        public List<ElementInShop> GetElements()
        {
            return _elementsInShop;
        }

        public void Buy(IElement element)
        {
            var itemOfShop = element.GetElement();
            Buy(itemOfShop);
        }

        public void Buy(ElementInShop element)
        {
            SpendGold(element.cost);
            var item = int.TryParse(ServiceLocator.Instance.GetService<ISaveData>().Get(element.key), out var health)
                ? health
                : 1;
            item += int.TryParse(element.value, out var healthIncrement)
                ? healthIncrement
                : 0;
            ServiceLocator.Instance.GetService<ISaveData>().Save(element.key, $"{item}");
        }
    }
}