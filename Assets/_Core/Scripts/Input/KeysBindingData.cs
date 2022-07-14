using UnityEngine;

namespace _Core.Scripts.Input
{
    [CreateAssetMenu(fileName = "KeysBindingData", menuName = "Keys bind data", order = 0)]
    public class KeysBindingData : ScriptableObject
    {
        [Header("Movement keys")]
        [SerializeField] private KeyCode jumpKey;
        [SerializeField] private KeyCode crouchKey;
        [SerializeField] private KeyCode sprintKey;
        [SerializeField] private KeyCode attack1Key;
        [SerializeField] private KeyCode attack2Key;
        [SerializeField] private KeyCode reloadKey;
        [SerializeField] private KeyCode interactKey;
        
        [Header("Interface keys")]
        [SerializeField] private KeyCode openInventoryKey;
        [SerializeField] private KeyCode openCraftKey;
        [SerializeField] private KeyCode openQuestsKey;
        [SerializeField] private KeyCode openSkillsKey;
        [SerializeField] private KeyCode openMapKey;
        [SerializeField] private KeyCode takeAllItemsKey;

        public KeyCode JumpKey => jumpKey;
        public KeyCode CrouchKey => crouchKey;
        public KeyCode SprintKey => sprintKey;
        public KeyCode Attack1Key => attack1Key;
        public KeyCode Attack2Key => attack2Key;
        public KeyCode ReloadKey => reloadKey;
        public KeyCode InteractKey => interactKey;

        public KeyCode OpenInventoryKey => openInventoryKey;
        public KeyCode OpenCraftKey => openCraftKey;
        public KeyCode OpenQuestsKey => openQuestsKey;
        public KeyCode OpenSkillsKey => openSkillsKey;
        public KeyCode OpenMapKey => openMapKey;
        public KeyCode TakeAllItemsKey => takeAllItemsKey;
    }
}