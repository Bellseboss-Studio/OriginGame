using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hexagons
{
    public class HexagonTemplate : MonoBehaviour
    {
        [SerializeField] private HexagonTemplateCollider collider;
        [SerializeField] private bool isHexagonBelongsThePlayer;
        private GameObject hexagonPrivate;
        private Hexagon referenceToConfig;

        public string Id => referenceToConfig.Position;

        public void Configure(Hexagon config, int positionX, int positionY)
        {
            collider.OnClickInHexagon += OnClick;
            referenceToConfig = config;
            referenceToConfig.Config($"{positionX}-{positionY}");
            hexagonPrivate = Instantiate(referenceToConfig.Prefab, transform);
            isHexagonBelongsThePlayer = ServiceLocator.Instance.GetService<IGlobalInformation>()
                .ThisHexagonIsWinToPlayer(referenceToConfig.Position);
        }

        private void OnClick()
        {
            if (isHexagonBelongsThePlayer)
            {
                ServiceLocator.Instance.GetService<ICameraController>().SetTarget(gameObject);   
            }
            else
            {
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
        }
    }
}