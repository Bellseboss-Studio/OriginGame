using System;
using UnityEngine;

namespace Hexagons
{
    public class HexagonTemplateCollider : MonoBehaviour
    {
        public Action OnClickInHexagon;

        public void Click()
        {
            OnClickInHexagon?.Invoke();
        }
    }
}