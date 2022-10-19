using System;
using UnityEngine;

namespace Hexagons
{
    public class HexagonTemplateCollider : MonoBehaviour
    {
        public Action OnClick;

        public void Click()
        {
            OnClick?.Invoke();
        }
    }
}