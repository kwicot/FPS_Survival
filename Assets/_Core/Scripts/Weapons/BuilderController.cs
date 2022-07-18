using System;
using System.Collections.Generic;
using System.Linq;
using _Core.Scripts.Build;
using _Core.Scripts.Input;
using _Core.Scripts.Items;
using _Core.Scripts.UI.Windows;
using Blocks.Core;
using UnityEngine;

namespace _Core.Scripts.Weapons
{
    public class BuilderController : ItemControllerBase
    {
        [SerializeField] private BlocksHolder blocksHolder;
        private Dictionary<string, GameObject> blocksMap => blocksHolder.BlockMap;

        private GameObject currentBlock;
        private int currentRotation;

        private Quaternion[] rotations = new[]
        {
            Quaternion.Euler(0, 0, 0),
            Quaternion.Euler(90, 0, 0),
            Quaternion.Euler(180, 0, 0),
            Quaternion.Euler(270, 0, 0),
            Quaternion.Euler(0, 0, 0),
            Quaternion.Euler(0, 90, 0),
            Quaternion.Euler(0, 180, 0),
            Quaternion.Euler(0, 270, 0),
            Quaternion.Euler(0, 0, 0),
            Quaternion.Euler(0, 0, 90),
            Quaternion.Euler(0, 0, 180),
            Quaternion.Euler(0, 0, 270)
        };
        protected override void Initialize()
        {
            EventManager.OnBlockSelected += OnSelectBlock;
            InputManager.Instance.InterfaceInput.OnBuildMenuKeyPress += OpenBuildMenu;
        }

        private void Update()
        {
            if (currentBlock)
            {
                if (GetHit(out var position))
                {
                    currentBlock.transform.position = Vector3Int.RoundToInt(position);
                    currentBlock.transform.rotation = rotations[currentRotation];
                }
            }
        }

        void PlaceBlock()
        {
            if (currentBlock)
            {
                var obj = Instantiate(currentBlock, currentBlock.transform.position, currentBlock.transform.rotation);
                obj.GetComponent<Collider>().enabled = true;
            }
        }

        void RemoveBlock()
        {
            
            
            
        }
        
        private void OnSelectBlock(string blockId)
        {
            if(currentBlock)
                Destroy(currentBlock);

            var prefab = GetPrefab(blockId);
            if (prefab == null)
            {
                Debug.Log($"Cant get block with id {blockId}");
                return;
            }

            currentBlock = Instantiate(prefab);
            currentBlock.GetComponent<Collider>().enabled = false;
        }
        GameObject GetPrefab(string id)
        {
            blocksMap.TryGetValue(id, out var block);
            return block ? block : null;
        }
        bool GetHit(out Vector3 position)
        {
            var ray = new Ray(playerController.PlayerLook.transform.position,
                playerController.PlayerLook.transform.forward);
            var hits = (Physics.RaycastAll(ray));
            foreach (var raycastHit in hits)
            {
                if (raycastHit.transform.CompareTag("Terrain"))
                {
                    var pos = raycastHit.point + (raycastHit.normal / 2);
                    position = Vector3Int.RoundToInt(pos);
                    return true;
                }

                if (raycastHit.transform.TryGetComponent(out BaseBlock block))
                {
                    var pos = raycastHit.transform.position + raycastHit.normal;
                    position = Vector3Int.RoundToInt(pos);
                    return true;
                }
            }

            position = Vector3.zero;
            return false;
        }
        private void OpenBuildMenu()
        {
            GameWindowsManager.Instance.OpenBuildMenu();
        }
        protected override void LeftMousePress() => PlaceBlock();
        protected override void RightMousePress() => RemoveBlock();
        protected override void LeftMouseRelease() { }
        protected override void RightMouseRelease() { }
        protected override void ReloadPressed()
        {
            currentRotation++;
            if (currentRotation == rotations.Length)
                currentRotation = 0;
        }
    }
}