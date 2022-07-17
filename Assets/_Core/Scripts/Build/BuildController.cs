using System;
using _Core.Scripts.Player;
using UnityEngine;

namespace _Core.Scripts.Build
{
    public class BuildController : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private BlocksHolder blocksHolder;
        
        

        private GameObject phantomObject;
            
        private bool isBuildMode = false;

        private void Start()
        {
            
        }

        private void Update()
        {
            if (isBuildMode)
            {

                var ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    var position = hit.point;
                    DrawPhantom(position);
                }
            }
        }
        // public void SelectBlock(int index)
        // {
        //     isBuildMode = true;
        //     phantomObject = Instantiate(blocksHolder.Blocks[index]);
        // }
        void DrawPhantom(Vector3 position)
        {
            phantomObject.transform.position = Vector3Int.RoundToInt(Vector3.positiveInfinity);
        }
        void PlaceFullBlock(Vector3 position)
        {
            if (playerController.Status.IsFreeBuild)
            {
                
            }
        }
        public void Place()
        {
            
        }
        public void Destroy()
        {
            
        }
    }
}