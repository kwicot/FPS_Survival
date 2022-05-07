using UnityEngine;

namespace _Core.Scripts.Input
{
    public class CarInput : InputBase
    {
        private RCC_CarControllerV3 carController;

        public void SetCar(RCC_CarControllerV3 carController)
        {
            this.carController = carController;
        }
        protected override void Enable()
        {
            if(!carController) return;
            
            carController.StartEngine();
            carController.canControl = true;
        }

        protected override void Disable()
        {
            if(!carController) return;
            
            carController.KillEngine();
            carController.canControl = false;
            carController = null;
        }
    }
}