using Object = UnityEngine.Object;

namespace Hexagons
{
    public class HexagonFactory
    {
        private readonly HexagonConfiguration _hexagonConfiguration;

        public HexagonFactory(HexagonConfiguration hexagonConfiguration)
        {
            _hexagonConfiguration = hexagonConfiguration;
        }

        public Hexagon Create(string id)
        {
            var hexagon = _hexagonConfiguration.GetHexagonPrefabById(id);
            return Object.Instantiate(hexagon);
        }

        public Hexagon Create(float height)
        {
            var hexagon = _hexagonConfiguration.GetHexagonPrefabByHeight(height);
            return Object.Instantiate(hexagon);
        }
    }
}