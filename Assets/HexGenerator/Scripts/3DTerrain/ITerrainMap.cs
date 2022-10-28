using Hexagons;
using UnityEngine;

namespace Terrains
{
    public interface ITerrainMap
    {
        HexagonTemplate GetCenter();
        Material GetDarkMaterial();
        Material GetConqueredMaterial();
        Canvas GetCanvasGeneral();
    }
}