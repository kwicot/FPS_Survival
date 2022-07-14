using System;
using System.Collections.Generic;
using Blocks.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.Build.BuildingsEditor
{
    public class BuildingEditorView : MonoBehaviour
    {
        [SerializeField] private GameObject panelRoot;
        [SerializeField] private GameObject blocksParent;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private BuildingEditor buildingEditor;

        private bool isOpen;

        public bool IsOpen => isOpen;
        private List<GameObject> cells = new List<GameObject>();
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            {
                if(isOpen)
                    Close();
                else
                    Open();
            }
        }

        public void Close()
        {
            isOpen = false;
            panelRoot.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Open()
        {
            isOpen = true;
            panelRoot.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

            foreach (var cell in cells)
                Destroy(cell);
            cells.Clear();
            
            foreach (var prefab in buildingEditor.BlockPrefabs)
            {
                var blockData = prefab.GetComponent<BaseBlock>();
                var cellObject = Instantiate(cellPrefab, blocksParent.transform);
                var textComponent = cellObject.GetComponentInChildren<TMP_Text>();
                var button = cellObject.GetComponentInChildren<Button>();

                textComponent.text = blockData.Name;
                
                button.onClick.AddListener(delegate
                {
                    buildingEditor.SelectBlock(prefab);
                    Close();
                });
                
                cells.Add(cellObject);
            }
        }
    }
}