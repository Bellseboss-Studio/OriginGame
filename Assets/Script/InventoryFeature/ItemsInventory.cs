using System.Collections.Generic;
using GameAudio;
using UnityEngine;

namespace SystemOfExtras
{
    public class ItemsInventory : MonoBehaviour , IItemsInventory
    {
        private GameObject _mainCamera;
        private List<InteractiveObject> _items;
        [SerializeField] private List<SpaceToItem> spacesToItems;
        [SerializeField] private Dialog dialogForNotItemSpace;
        [SerializeField] private GameObject backpack;
        private bool _backpackShowed;
        private bool _movingBackpack;
        [SerializeField] private float moveInY, animationDuration;
        private int _itemsSaved;
        private Transform referenceOfPlayer;

        private void Awake()
        {
            _items = new List<InteractiveObject>();
            var items1 = FindObjectsOfType<Item>();
            foreach (var item in items1)
            {
                _items.Add(item.InteractiveObject);
            }
        }

        public void SaveItem(Item item)
        {
            _itemsSaved = 0;
            var savedItem = false;
            foreach (var spaceToItem in spacesToItems)
            {
                if (spaceToItem.CurrentItem)
                {
                    _itemsSaved++;
                    continue;
                }

                if (savedItem) continue;
                savedItem = true;
                spaceToItem.CurrentItem = item;
                item.transform.position = spaceToItem.transform.position;
                item.transform.SetParent(backpack.transform);
                item.transform.rotation = backpack.transform.rotation;
                item.PutInTheBackpack();
            }

            if (_itemsSaved>=2)
            {
                foreach (var item1 in _items)
                {
                    item1.SetDialogo(dialogForNotItemSpace);
                }
            }
            //Si no hay espacio para el item
        }

        public bool SearchItemForId(string id)
        {
            foreach (var spaceToItem in spacesToItems)
            {
                if (spaceToItem.CurrentItem!= null && spaceToItem.CurrentItem.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasSpace()
        {
            return _itemsSaved < 2;
        }

        public int RemoveItemById(string itemId)
        {
            int countItemsRemove = 0;
            foreach (var spaceToItem in spacesToItems)
            {
                if (spaceToItem.CurrentItem!= null && spaceToItem.CurrentItem.Id == itemId)
                {
                    _items.Remove(spaceToItem.CurrentItem.InteractiveObject);
                    Destroy(spaceToItem.CurrentItem.gameObject);
                    countItemsRemove++;
                    RestoreItemsDialog();
                }
            }
            return countItemsRemove;
        }

        public Transform GetTransformPlayer()
        {
            return referenceOfPlayer;
        }

        public void ThrowItem(int itemPosition)
        {
            if (spacesToItems[itemPosition].CurrentItem == null)
            {
                /*ServiceLocator.Instance.GetService<IDialogSystem>(). OpenDialog(dialogToNotItemToThrow.Id);*/
            }
            else
            {
                Debug.Log($"tirando item{itemPosition}");
                ServiceLocator.Instance.GetService<InteractablesSounds>().PlaySound("DiscardItem");
                _items.Remove(spacesToItems[itemPosition].CurrentItem.InteractiveObject);
                Destroy(spacesToItems[itemPosition].CurrentItem.gameObject);
                RestoreItemsDialog();
                _itemsSaved--;
            }
        }

        private void RestoreItemsDialog()
        {
            foreach (var item in _items)
            {
                item.RestoreDialog();
            }
        }

    }
}