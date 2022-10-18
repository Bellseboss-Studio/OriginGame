using Hexagons;
using UnityEngine;

namespace Terrains
{
    public class SpawnerTerrain : MonoBehaviour
    {
        [SerializeField] private HexagonConfiguration _hexagonConfiguration;
        private HexagonFactory _hexagonFactory;

        private void Awake()
        {
            _hexagonFactory = new HexagonFactory(Instantiate(_hexagonConfiguration));
        }

        public Hexagon CreateByHeight(float height)
        {
            return _hexagonFactory.Create(height);
        }

        public Hexagon CreateById(string name)
        {
            return _hexagonFactory.Create(name);
        }
    }
}