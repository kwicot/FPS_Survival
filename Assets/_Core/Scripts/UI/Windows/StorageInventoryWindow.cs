using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using _Core.Scripts.UI.MainMenu;
using UnityEngine;

namespace _Core.Scripts.UI.Windows
{
    public class StorageInventoryWindow : InventoryWindow
    {
        [SerializeField] private StorageInventoryWindow secondInventoryWindow;

        public bool MoveToAdditional(Item item, out AddResult result)
        {
            return secondInventoryWindow.AddItem(item, out result);
        }

        protected override void OnOpen()
        {
            base.OnOpen();
        }

        protected override void OnClose()
        {
            base.OnClose();
        }
    }
}