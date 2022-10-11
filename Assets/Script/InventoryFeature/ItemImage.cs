using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SystemOfExtras
{
    public class ItemImage : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text description;
        
        public void Configure(Sprite sprite, string itemDescription)
        {
            image.sprite = sprite;
            description.text = itemDescription;
        }
    }
}