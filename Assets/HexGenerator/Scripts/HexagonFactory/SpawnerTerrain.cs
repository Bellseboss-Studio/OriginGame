using Hexagons;
using UnityEngine;

namespace Terrains
{
    public class SpawnerTerrain : MonoBehaviour
    {
        [SerializeField] private HexagonConfiguration _hexagonConfiguration;
        [SerializeField] private HexagonTemplate template;
        private HexagonFactory _hexagonFactory;

        private void Awake()
        {
            _hexagonFactory = new HexagonFactory(Instantiate(_hexagonConfiguration));
        }

        public HexagonTemplate CreateByHeight(float height)
        {
            var hexagonTemplate = Instantiate(template);
            hexagonTemplate.Configure(_hexagonFactory.Create(height));
            return hexagonTemplate;
        }

        public HexagonTemplate CreateById(string name)
        {
            var hexagonTemplate = Instantiate(template);
            hexagonTemplate.Configure(_hexagonFactory.Create(name));
            return hexagonTemplate;
        }
    }
}