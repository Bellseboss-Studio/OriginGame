using UnityEngine;

namespace SystemOfExtras
{
    public interface IItemsInventory
    {
        void SaveItem(Item item);
        void ThrowItem(int itemPosition);
        bool SearchItemForId(string id);
        bool HasSpace();
        int RemoveItemById(string itemId);
        Transform GetTransformPlayer();
    }
}