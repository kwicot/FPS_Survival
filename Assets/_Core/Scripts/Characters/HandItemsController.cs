using System;
using _Core.Scripts.Items;
using UnityEngine;

namespace _Core.Scripts.Player
{
    public class HandItemsController : MonoBehaviour
    {
        [SerializeField] private Transform prefabsParent;

        private WeaponItem currentItem;
        private GameObject currentPrefab;
        private void Start()
        {
            EventManager.OnItemSelected += OnItemSelected;
        }

        private void OnItemSelected(WeaponItem selectedItem)
        {
            if(currentItem == selectedItem) return;

            if (currentItem != null)
            {
                Destroy(currentPrefab);
                currentItem = null;
            }

            if (selectedItem != null)
            {
                currentItem = selectedItem;
                var weaponConfig = selectedItem.WeaponConfig;
                currentPrefab = Instantiate(weaponConfig.WeaponPrefab, prefabsParent);
                currentPrefab.transform.localPosition = weaponConfig.SpawnPosition;
            }
        }
    }
}