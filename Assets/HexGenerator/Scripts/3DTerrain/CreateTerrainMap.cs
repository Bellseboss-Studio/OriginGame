using System;
using Hexagons;
using SystemOfExtras;
using UnityEngine;
using UnityEngine.TerrainTools;

namespace Terrains
{
    public class CreateTerrainMap : MonoBehaviour, ITerrainMap
    {
        [SerializeField] private MapGenerator mapGenerator;
        [SerializeField] private SpawnerTerrain spawner;
        [SerializeField] private float offsetHeight;
        private float[,] map;
        private HexagonTemplate[,] mapGameObject;

        public void CreateMap()
        {
            map = mapGenerator.GenerateMap();
            mapGameObject = new HexagonTemplate[map.GetLength(0), map.GetLength(1)];
            
            float radio = 0.86602f;//Z
            float altura = 1.5f;//X

            float alturaAlter = 0;
            float radioAlter = 0;
            
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    HexagonTemplate byHeight;
                    try
                    {
                        byHeight = spawner.CreateByHeight(map[i, j]);
                    }
                    catch (Exception e)
                    {
                        Debug.Log($"{e.Message}");
                        byHeight = spawner.CreateById("2");
                    }

                    mapGameObject[i, j] = byHeight;
                    byHeight.transform.position = new Vector3(alturaAlter, map[i, j] * offsetHeight, radioAlter);
                    alturaAlter += altura * 2;
                }
                radioAlter += radio;
                if ((i % 2) == 0)
                {
                    alturaAlter = altura;
                }
                else
                {
                    alturaAlter = 0;
                }
            }
        }

        public HexagonTemplate GetCenter()
        {
            var middle1 = map.GetLength(0)/2;
            var middle2 = map.GetLength(1)/2;
            return mapGameObject[middle1, middle2];
        }
    }
}