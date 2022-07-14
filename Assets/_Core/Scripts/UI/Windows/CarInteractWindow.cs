using _Core.Scripts.InventorySystem;
using _Core.Scripts.UI.MainMenu;
using UnityEngine;

namespace _Core.Scripts.UI.Windows
{
    public class CarInteractWindow : WindowBase
    {
        private CarController carController;
        
        protected override void OnOpen()
        {
            
        }

        protected override void OnClose()
        {
            
        }

        public void SetCarController(CarController car)
        {
            this.carController = car;
        }
        
        public void Enter()
        {
            EventManager.OnEnterCar?.Invoke(carController);
        }

        public void OpenInventory()
        {
            GameWindowsManager.Instance.OpenStorageInventory(carController.Inventory);
        }
    }
}