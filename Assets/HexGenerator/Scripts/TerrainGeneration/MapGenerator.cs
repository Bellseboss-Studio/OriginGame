using System;
using UnityEngine;

namespace Terrains
{
    public class MapGenerator : MonoBehaviour
    {
        public int mapWidth;
        public int mapHeight;
        public float noiseScale;

        public int octaves;
        [Range(0, 1)]
        public float persistance;
        public float lacunarity;

        public int seed;
        public Vector2 offset;

        public float[,] GenerateMap()
        {
            float[,] noiseMap = NoiseMapGeneration.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
            return noiseMap;
        }
    }
}