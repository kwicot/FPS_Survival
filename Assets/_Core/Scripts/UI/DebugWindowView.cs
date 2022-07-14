using _Core.Scripts.Player;
using UnityEngine;

namespace _Core.Scripts.UI
{
    public class DebugWindowView : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;

        public void ChangeFlyMode(bool value)
        {
            playerController.Status.IsFlyMode = value;
        }

        public void ChangeBuildMode(bool value)
        {
            playerController.Status.IsFreeBuild = value;
        }
    }
}