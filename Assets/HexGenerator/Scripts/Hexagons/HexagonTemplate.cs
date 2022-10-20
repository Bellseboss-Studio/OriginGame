using SystemOfExtras;
using UnityEngine;

namespace Hexagons
{
    public class HexagonTemplate : MonoBehaviour
    {
        [SerializeField] private HexagonTemplateCollider collider;
        private GameObject hexagonPrivate;
        private Hexagon referenceToConfig;
        public void Configure(Hexagon config)
        {
            collider.OnClick += OnClick;
            referenceToConfig = config;
            hexagonPrivate = Instantiate(referenceToConfig.Prefab, transform);
        }

        private void OnClick()
        {
            Debug.Log($"Click in {referenceToConfig.Id}");
            ServiceLocator.Instance.GetService<ICameraController>().SetTarget(gameObject);
        }
    }
}