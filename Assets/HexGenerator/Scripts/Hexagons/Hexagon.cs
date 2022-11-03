using UnityEngine;

namespace Hexagons
{
    [CreateAssetMenu(menuName = "Custom/Hexagon single")]
    public class Hexagon : ScriptableObject
    {
        [SerializeField] private string id;

        [SerializeField] private GameObject hexagonPrefab, prefabConquered;

        [SerializeField] private float minHeight, maxHeight;
        [SerializeField] private int cost;
        private string position;

        public string Position => position;

        public float Min => minHeight;
        public float Max => maxHeight;

        public string Id => id;
        public GameObject Prefab => hexagonPrefab;
        public int Cost => cost;
        public GameObject PrefabConquered => prefabConquered;

        public virtual void Orientation()
        {
            
        }

        public void Config(string position)
        {
            this.position = position;
        }
    }
}