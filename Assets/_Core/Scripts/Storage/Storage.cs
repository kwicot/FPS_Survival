using Player.Core;
using UnityEngine;

namespace _Core.Scripts.Storage
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;

        public Inventory Inventory => inventory;
    }
}