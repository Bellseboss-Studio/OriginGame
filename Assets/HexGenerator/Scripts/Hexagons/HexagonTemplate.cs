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
            hexagonPrivate = Instantiate(referenceToConfig.Prefab, transform);
            isHexagonBelongsThePlayer = ServiceLocator.Instance.GetService<IGlobalInformation>()
                .ThisHexagonIsWinToPlayer(referenceToConfig.Position);
            camera = ServiceLocator.Instance.GetService<ICameraController>().GetCamera();
            _totalCost = referenceToConfig.Cost; //if they have a multiplic value
        }

        private void OnClick()
        {
            if (isHexagonBelongsThePlayer)
            {
                ServiceLocator.Instance.GetService<ICameraController>().SetTarget(gameObject);   
            }
            else
            {
                ServiceLocator.Instance.GetService<IGlobalInformation>().SpendGold(_totalCost);
                ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
                {
                    ServiceLocator.Instance.GetService<IGlobalInformation>().HexagonToBet(this);
                    SceneManager.LoadScene(4);
                });   
            }
        }

        public void PlayerWinThisHexagon()
        {
            isHexagonBelongsThePlayer = true;
            hexagonPrivate.GetComponent<MeshRenderer>().materials = referenceToConfig.OriginalMaterials();
        }

        public bool IsPlayerHexagon()
        {
            return isHexagonBelongsThePlayer;
        }

        public void CanConquer()
        {
            Debug.Log($"Try conquer {referenceToConfig.Position}");
            if (isVisibleForPlayer || isHexagonBelongsThePlayer) return;
            isVisibleForPlayer = true;
            //hexagonPrivate.GetComponent<MeshRenderer>().materials = new[] { _terrainMap.GetConqueredMaterial() };
            var costTemplate = Instantiate(panel, _terrainMap.GetCanvasGeneral().transform, true);
            costTemplate.transform.position = pointToStayPanel.transform.position;
            costTemplate.Using($"Cost: {_totalCost} gold");
            costTemplate.transform.LookAt(_terrainMap.GetCanvasGeneral().transform);
        }

        public bool IsVisibleForPlayer()
        {
            return isVisibleForPlayer;
        }

        public void HideToPlayer()
        {
            //Debug.Log($"Try Hide {referenceToConfig.Position}");
            hexagonPrivate.GetComponent<MeshRenderer>().materials = new[] { _terrainMap.GetDarkMaterial() };
        }
    }
}