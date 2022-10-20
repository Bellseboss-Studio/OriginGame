using UnityEngine;

namespace Hexagons
{
    [CreateAssetMenu(menuName = "Custom/Hexagon single")]
    public class Hexagon : ScriptableObject
    {
        [SerializeField] private string id;

        [SerializeField] private GameObject hexagonPrefab;

        [SerializeField] private float minHeight, maxHeight;

        public float Min => minHeight;
        public float Max => maxHeight;

        public string Id => id;
        public GameObject Prefab => hexagonPrefab;

        public virtual void Orientation()
        {
            
        }
    }
}