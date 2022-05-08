using UnityEngine;

namespace _Core.Scripts.UI
{
    public class StorageInventoryView : InventoryView
    {
        [SerializeField] private InventoryView playerInventoryView;


        public override void Open()
        {
            base.Open();
            playerInventoryView.Open();
        }
    }
}