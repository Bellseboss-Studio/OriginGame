using SystemOfExtras;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hexagons
{
    public class HexagonTemplate : MonoBehaviour
    {
        [SerializeField] private HexagonTemplateCollider collider;
        private GameObject hexagonPrivate;
        private Hexagon referenceToConfig;
        public void Configure(Hexagon config)
        {
            collider.OnClickInHexagon += OnClick;
            referenceToConfig = config;
            hexagonPrivate = Instantiate(referenceToConfig.Prefab, transform);
        }

        private void OnClick()
        {
            Debug.Log($"Click in {referenceToConfig.Id}");
            //ServiceLocator.Instance.GetService<ICameraController>().SetTarget(gameObject);
            ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
            {
                SceneManager.LoadScene(4);
            });
            /*ServiceLocator.Instance.GetService<IMessagingService>().ShowYesAndNo("title", "message",() =>
            {

            }, () =>
            {

            });*/
        }
    }
}