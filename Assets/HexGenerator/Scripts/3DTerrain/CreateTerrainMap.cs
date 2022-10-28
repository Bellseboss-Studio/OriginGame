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
        [SerializeField] private Material darkMaterial, conquerMaterial;
        [SerializeField] private Canvas canvasGeneral;
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
                        byHeight = spawner.CreateByHeight(map[i, j], i, j, this);
                    }
                    catch (Exception e)
                    {
                        //Debug.Log($"{e.Message}");
                        byHeight = spawner.CreateById("2", i, j, this);
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
            //Create the piramide to player
            
            var middle1 = map.GetLength(0)/2;
            var middle2 = map.GetLength(1)/2;
            var position = mapGameObject[middle1, middle2].gameObject.transform.position; 
            Destroy(mapGameObject[middle1, middle2].gameObject);
            mapGameObject[middle1, middle2] = spawner.CreateById("0", middle1, middle2, this);
            mapGameObject[middle1, middle2].PlayerWinThisHexagon();
            mapGameObject[middle1, middle2].transform.position = position;
            
            //ilumint the next hexagons for the close to conquister hexagons
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (mapGameObject[i, j].IsPlayerHexagon())
                    {
                        try
                        {
                            mapGameObject[i - 2, j].CanConquer();//ok
                            mapGameObject[i + 2, j].CanConquer();//ok
                            mapGameObject[i - 1, j].CanConquer();//ok
                            mapGameObject[i + 1, j].CanConquer();//ok
                            mapGameObject[i + 1, j - 1].CanConquer();//ok
                            mapGameObject[i - 1, j + 1].CanConquer();//ok

                            Debug.Log($"{mapGameObject[i, j].Id} convert: " +
                                      $"{mapGameObject[i - 2, j].Id} - " +
                                      $"{mapGameObject[i + 2, j].Id} - " +
                                      $"{mapGameObject[i - 1, j].Id} - " +
                                      $"{mapGameObject[i + 1, j].Id} - " +
                                      $"{mapGameObject[i + 1, j - 1].Id} - " +
                                      $"{mapGameObject[i - 1, j + 1].Id}");
                        }
                        catch (Exception e)
                        {
                            //ignore
                        }
                    }else
                    {
                        mapGameObject[i, j].HideToPlayer();
                    }
                }
            }
        }

        public HexagonTemplate GetCenter()
        {
            var middle1 = map.GetLength(0)/2;
            var middle2 = map.GetLength(1)/2;
            return mapGameObject[middle1, middle2];
        }

        public Material GetDarkMaterial()
        {
            return darkMaterial;
        }

        public Material GetConqueredMaterial()
        {
            return conquerMaterial;
        }

        public Canvas GetCanvasGeneral()
        {
            return canvasGeneral;
        }
    }
}