using System;
using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using Terrains;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hexagons
{
    public class HexagonTemplate : MonoBehaviour
    {
        [SerializeField] private HexagonTemplateCollider collider;
        [SerializeField] private bool isHexagonBelongsThePlayer, isVisibleForPlayer;
        [SerializeField] private string positionHexagon;
        [SerializeField] private CostTemplate panel;
        [SerializeField] private GameObject pointToStayPanel;
        [SerializeField]private int sceneBlackJack, sceneRoulette;
        [SerializeField] private GameObject hide, canConquer;
        private GameObject hexagonPrivate;
        private Hexagon referenceToConfig;
        private ITerrainMap _terrainMap;
        private GameObject camera;
        private bool isReady;
        private int _totalCost;

        public string Id => referenceToConfig.Position;

        public void Configure(Hexagon config, int positionX, int positionY,ITerrainMap terrainMap)
        {
            _terrainMap = terrainMap;
            collider.OnClickInHexagon += OnClick;
            referenceToConfig = config;
            referenceToConfig.Config($"{positionX}-{positionY}");
            positionHexagon = referenceToConfig.Position;
            isHexagonBelongsThePlayer = ServiceLocator.Instance.GetService<IGlobalInformation>()
                .ThisHexagonIsWinToPlayer(referenceToConfig.Position);
            camera = ServiceLocator.Instance.GetService<ICameraController>().GetCamera();
            _totalCost = referenceToConfig.Cost; //if they have a multiplic value
            hexagonPrivate = Instantiate(isHexagonBelongsThePlayer ? referenceToConfig.PrefabConquered : referenceToConfig.Prefab, transform);
        }

        private void OnClick()
        {
            if (!isVisibleForPlayer && !isHexagonBelongsThePlayer) return;
            if (isHexagonBelongsThePlayer)
            {
                ServiceLocator.Instance.GetService<ICameraController>().SetTarget(gameObject);   
            }
            else
            {
                try
                {
                    ServiceLocator.Instance.GetService<IGlobalInformation>().SpendGold(_totalCost);
                    //Calculate the health and damage for enemy
                    ServiceLocator.Instance.GetService<IStatsInformation>().CalculateStatsForEnemy(referenceToConfig);
                    ServiceLocator.Instance.GetService<IGlobalInformation>().HexagonToBet(this);
                    ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
                    {
                        SceneManager.LoadScene(sceneBlackJack);
                    });
                }
                catch (Exception e)
                {
                    //no le alacanzo el oro
                    ServiceLocator.Instance.GetService<ILoadScene>().ShowMessageWithTwoButton(
                        "Gold is not enough", 
                        "If you wanna to play, you can play roulette or tweet the game to win gold. What do you want to do?", 
                        "Play Roullete", () =>
                        {
                            //TODO go to roulette
                            ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
                            {
                                SceneManager.LoadScene(sceneRoulette);
                            });
                        }, 
                        "Tweet the game", 
                        ServiceLocator.Instance.GetService<IGlobalInformation>().TweetAction(), () =>
                        {
                            //TODO whats happend if the cancel way
                        });
                }
            }
        }

        public void PlayerWinThisHexagon()
        {
            isHexagonBelongsThePlayer = true;
        }

        public bool IsPlayerHexagon()
        {
            return isHexagonBelongsThePlayer;
        }

        public void CanConquer()
        {
            Debug.Log($"Try conquer {referenceToConfig.Position}");
            hide.SetActive(false);
            if (isVisibleForPlayer || isHexagonBelongsThePlayer) return;
            isVisibleForPlayer = true;
            canConquer.SetActive(true);
            //hexagonPrivate.GetComponent<MeshRenderer>().materials = new[] { _terrainMap.GetConqueredMaterial() };
            /*var costTemplate = Instantiate(panel, _terrainMap.GetCanvasGeneral().transform, true);
            costTemplate.transform.position = pointToStayPanel.transform.position;
            costTemplate.Using($"Cost: {_totalCost} gold");
            costTemplate.transform.LookAt(_terrainMap.GetCanvasGeneral().transform);
            */
            hide.SetActive(false);
        }

        public bool IsVisibleForPlayer()
        {
            return isVisibleForPlayer;
        }

        public void HideToPlayer()
        {
            //Debug.Log($"Try Hide {referenceToConfig.Position}");
            //hexagonPrivate.GetComponent<MeshRenderer>().materials = new[] { _terrainMap.GetDarkMaterial() };
            //hide.SetActive(true);
        }

        public Hexagon GetHexagon()
        {
            return referenceToConfig;
        }
    }
}