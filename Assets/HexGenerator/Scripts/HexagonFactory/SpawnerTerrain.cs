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

        public HexagonTemplate CreateByHeight(float height, int x, int y, ITerrainMap createTerrainMap)
        {
            var hexagonTemplate = Instantiate(template);
            hexagonTemplate.Configure(_hexagonFactory.Create(height), x, y,createTerrainMap);
            return hexagonTemplate;
        }

        public HexagonTemplate CreateById(string name, int x, int y, ITerrainMap createTerrainMap)
        {
            var hexagonTemplate = Instantiate(template);
            hexagonTemplate.Configure(_hexagonFactory.Create(name), x, y,createTerrainMap);
            return hexagonTemplate;
        }
    }
}