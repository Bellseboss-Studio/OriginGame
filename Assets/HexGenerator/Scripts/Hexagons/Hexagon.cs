using UnityEngine;
using UnityEngine.Tilemaps;

namespace Hexagons
{
    public abstract class Hexagon : MonoBehaviour
    {
        [SerializeField]
        public string id;

        [SerializeField] private float minHeight, maxHeight;

        public float Min => minHeight;
        public float Max => maxHeight;

        public virtual void Orientation()
        {
            
        }
    }
}