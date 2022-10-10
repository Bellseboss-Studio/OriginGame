using UnityEngine;

namespace SystemOfExtras
{
    public class Item : InteractiveObjectFather
    {
        [SerializeField] private string itemId;
        [SerializeField] private GameObject normalModel, backpackModel;
        public string Id => itemId;
        [SerializeField] private InteractiveObject interactiveObject;

        public InteractiveObject InteractiveObject => interactiveObject;

        private Dialog _dialog;
        protected override void ActionEventCustom()
        {
            ServiceLocator.Instance.GetService<IItemsInventory>().SaveItem(this);
        }

        public void PutInTheBackpack()
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            normalModel.SetActive(false);
            backpackModel.SetActive(true);
            Debug.Log("Goes in Backpack");
        }
        
        public void TakeOutTheBackpack()
        {
            normalModel.SetActive(true);
            backpackModel.SetActive(false);
        }
        
        public void SetDialogo(Dialog cambioDeDialogoDeLlave)
        {
            dialogToAction = cambioDeDialogoDeLlave;
        }
    }
}