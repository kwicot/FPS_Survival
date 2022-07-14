using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using _Core.Scripts.Input;
using Blocks.Core;
using Newtonsoft.Json;
using SFB;
using UnityEngine;

namespace _Core.Scripts.Build.BuildingsEditor
{
    public class BuildingEditor : MonoBehaviour
    {
        [Header("Movement")] 
        [SerializeField] private GameObject camera;
        [SerializeField] private float mouseSensitivity = 100f;
        [SerializeField] private float maxAngleX;
        [SerializeField] private float minAngleX;
        [SerializeField] private float moveSpeed;
        private float xRotation = 0;

        [Header("Build")]        
        [SerializeField] private GameObject[] blockPrefabs;
        [SerializeField] private float buildDistance;
        [SerializeField] private BuildingEditorView buildingView;
        

        private List<BaseBlock> blocksOnMap;
        private GameObject currentBlock;

        public GameObject[] BlockPrefabs => blockPrefabs;

        private void Start()
        {
            SelectBlock(blockPrefabs[0]);
            blocksOnMap = new List<BaseBlock>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if(buildingView.IsOpen) return;
            
             UpdateMovement();
             UpdateRotation();
             BuildUpdate();
             
             
             if(UnityEngine.Input.GetKeyDown(KeyCode.Delete))
                 DestroyAllBlocks();
             
        }

        #region Movement

        void UpdateMovement()
        {
            var horizontal = UnityEngine.Input.GetAxis("Horizontal");
            var vertical = UnityEngine.Input.GetAxis("Vertical");

            var jump = UnityEngine.Input.GetKey(KeyCode.Space);
            var crouch = UnityEngine.Input.GetKey(KeyCode.LeftShift);

            float yCoord = 0;
            if (jump)
                yCoord += 1;
            if (crouch)
                yCoord -= 1;
            
            //Debug.Log(jump);

            
            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            move.y = yCoord;
            
            transform.position += move * moveSpeed * Time.deltaTime;
        }
        void UpdateRotation()
        {
            var x = UnityEngine.Input.GetAxis("Mouse X");
            var y = UnityEngine.Input.GetAxis("Mouse Y");
            
            var mouseX = x * mouseSensitivity * Time.deltaTime;
            var mouseY = y * mouseSensitivity * Time.deltaTime;
            
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minAngleX, maxAngleX);
            
            camera.transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
            transform.Rotate(Vector3.up * mouseX);
        }

        #endregion

        void BuildUpdate()
        {
            var ray = new Ray(camera.transform.position, camera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, buildDistance))
            {
                Vector3 position;
                if (hit.transform.CompareTag("Terrain"))
                {
                    position = hit.point + hit.normal;
                    position = Vector3Int.RoundToInt(position);
                }
                else
                    position = hit.transform.position + hit.normal;

                currentBlock.transform.position = position;

                if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if (hit.transform.TryGetComponent(out BaseBlock block))
                    {
                        blocksOnMap.Remove(block);
                        Destroy(block.gameObject);
                        return;
                    }
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
                {
                    
                    var block = Instantiate(currentBlock, currentBlock.transform.position,
                        currentBlock.transform.rotation);
                    block.GetComponent<Collider>().enabled = true;
                    blocksOnMap.Add(block.GetComponent<BaseBlock>());
                }
            }
            else
                currentBlock.transform.position = new Vector3(100000, 100000, 100000);



        }

        public void SelectBlock(GameObject prefab)
        {
            if(currentBlock)
                Destroy(currentBlock);

            currentBlock = Instantiate(prefab);
            currentBlock.GetComponent<Collider>().enabled = false;
        }

        public void DestroyAllBlocks()
        {
            foreach (var baseBlock in blocksOnMap)
                Destroy(baseBlock.gameObject);
            
            blocksOnMap.Clear();
        }


        public void Save()
        {
            //TODO filebrowser
            var blocksData = new List<BlockModel>();
            foreach (var blockOnMap in blocksOnMap)
            {
                var blockData = new BlockModel();
                blockData.id = blockOnMap.ID;
                blockData.position = blockOnMap.transform.position;
                blockData.rotation = blockOnMap.transform.rotation.eulerAngles;
                
                blocksData.Add(blockData);
            }

            var dataArray = blocksData.ToArray();
            byte[] bytesData;
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, dataArray);
                bytesData = ms.ToArray();
            }

            var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "");
            if(File.Exists(path))
                File.Delete(path);
            
            File.WriteAllBytes(path,bytesData);
            buildingView.Close();
        }

        public void Load()
        {
            DestroyAllBlocks();
            buildingView.Close();
            Cursor.lockState = CursorLockMode.None;
            
            var paths = StandaloneFileBrowser.OpenFilePanel("Save File", "", "", false);
            Cursor.lockState = CursorLockMode.Locked;

            if (paths != null && paths.Length > 0)
            {
                if (File.Exists(paths[0]))
                {
                    var bytes = File.ReadAllBytes(paths[0]);
                    BlockModel[] blocksData;
                    using (var memStream = new MemoryStream())
                    {
                        var binForm = new BinaryFormatter();
                        memStream.Write(bytes, 0, bytes.Length);
                        memStream.Seek(0, SeekOrigin.Begin);
                        var obj = (BlockModel[])binForm.Deserialize(memStream);
                        blocksData = obj;
                    }

                    foreach (var blockModel in blocksData)
                    {
                        var prefab = GetPrefabById(blockModel.id);
                        if(prefab == null) Debug.LogError($"Cant get block with id {blockModel.id}");
                        else
                        {
                            var block = Instantiate(prefab);
                            block.transform.position = blockModel.position;
                            block.transform.rotation = Quaternion.Euler(blockModel.rotation);
                            blocksOnMap.Add(block.GetComponent<BaseBlock>());
                        }
                    }
                }
            }
        }

        GameObject GetPrefabById(string id)
        {
            foreach (var prefab in blockPrefabs)
            {
                if (prefab.GetComponent<BaseBlock>().ID == id)
                    return prefab;
            }

            return null;
        }
    }
}