using UnityEngine;

namespace Hexagons
{
    [CreateAssetMenu(menuName = "Custom/Hexagon single")]
    public class Hexagon : ScriptableObject
    {
        [SerializeField] private string id;

        [SerializeField] private GameObject hexagonPrefab;

        [SerializeField] private float minHeight, maxHeight;
        private string position;

        public string Position => position;

        public float Min => minHeight;
        public float Max => maxHeight;

        public string Id => id;
        public GameObject Prefab => hexagonPrefab;

        public virtual void Orientation()
        {
            
        }

        public void Config(string position)
        {
            this.position = position;
        }
    }
}