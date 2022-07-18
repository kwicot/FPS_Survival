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
        [SerializeField] private LayerMask raycastLayer;
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
                if (GetHit(out Vector3 position))
                {
                    currentBlock.transform.position = position;
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
            if (GetHit(out RaycastHit hit))
            {
                if(hit.transform.TryGetComponent(out BaseBlock block))
                    Destroy(block.gameObject);
            }
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
            
            if (GetHit(out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent(out BaseBlock block))
                {
                    Debug.Log($"Hit in {hit.transform.name}");
                    //Debug.Log("Its block");
                    var curBlockScript = currentBlock.GetComponent<BaseBlock>();
                    var hitBlockScript = hit.transform.GetComponent<BaseBlock>();
                    Vector3 pos;

                    if (curBlockScript.CellSizeDevide != hitBlockScript.CellSizeDevide)
                        pos = hit.point + (hit.normal * (currentBlock.transform.localScale.x - 0.05f));
                    else
                        pos = hit.transform.position + (hit.normal * currentBlock.transform.localScale.x);

                    position = Round(pos, currentBlock.GetComponent<BaseBlock>().CellSizeDevide);
                    return true;
                }
                else if (hit.transform.CompareTag("Terrain"))
                {
                    //Debug.Log("Its terrain");
                    var pos = hit.point + hit.normal;
                    position = Round(pos, currentBlock.GetComponent<BaseBlock>().CellSizeDevide);
                    return true;
                }
            }

            //Debug.Log($"No need hits. Other hits {hits.Length}");
            position = Vector3.zero;
            return false;
        }
        bool GetHit(out RaycastHit hit)
        {
            var ray = new Ray(playerController.PlayerLook.transform.position,
                playerController.PlayerLook.transform.forward);

            Debug.DrawRay(ray.origin,ray.direction,Color.red);
            if (Physics.Raycast(ray, out hit, raycastLayer))
                    return true;
            return false;
        }
        bool GetHit<T>(out T component)
        {
            var ray = new Ray(playerController.PlayerLook.transform.position,
                playerController.PlayerLook.transform.forward);

            Debug.DrawRay(ray.origin,ray.direction,Color.red);
            if (Physics.Raycast(ray, out var hit, raycastLayer))
            {
                if(hit.transform.TryGetComponent(out component))
                    return true;
            }

            component = default(T);
            return false;
        }

        Vector3 Round(Vector3 origin, BuildCellSize cellSizeDevide)
        {
            var x = Round(origin.x, cellSizeDevide);
            var y = Round(origin.y, cellSizeDevide);
            var z = Round(origin.z, cellSizeDevide);

            return new Vector3(x, y, z);
        }

        float Round(float origin, BuildCellSize cellSizeDevide)
        {
            float res;
            float difference;
            float ost;
            var inverse = origin < 0;

            origin = Mathf.Abs(origin);
            
            switch (cellSizeDevide)
            {
                case BuildCellSize.normal:
                    
                    difference = origin % 1;
                    ost = 1 - difference;
                    if (difference < 0.5f)
                        res = origin - difference;
                    else
                        res = origin + ost;

                        break;
                
                case BuildCellSize.small: 
                    
                    difference = origin % 0.5f;
                    ost = 0.5f - difference;

                    if (difference <= 0.5f)
                        res = origin - difference;
                    else
                        res = origin + ost;

                    
                    res += 0.25f;
                    
                    Debug.Log($"Origin {origin} dif {difference} res {res}");
                    break;
                case BuildCellSize.verySmall:
                    
                    difference = origin % 0.25f;
                    ost = 0.25f - difference;

                    if (difference <= 0.25f)
                        res = origin - difference;
                    else
                        res = origin + ost;

                    
                    res += 0.125f;
                    
                    Debug.Log($"Origin {origin} dif {difference} res {res}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellSizeDevide), cellSizeDevide, null);
            }
            //var res = (float)Math.Round(origin * cellSizeDevide, MidpointRounding.AwayFromZero) / cellSizeDevide;
            if (inverse)
                res *= -1;
            return res;
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