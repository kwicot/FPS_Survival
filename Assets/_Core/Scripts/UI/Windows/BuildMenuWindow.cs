using _Core.Scripts.Build;
using _Core.Scripts.UI.MainMenu;
using Blocks.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.UI.Windows
{
    public class BuildMenuWindow : WindowBase
    {
        [SerializeField] private BuildConfig buildConfig;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Transform cellParent;

        private string selectedBlock;
        protected override void OnOpen()
        {
            DrawAllBlocks();
        }

        protected override void OnClose()
        {
            
        }

        public void DrawAllBlocks()
        {
            ClearPanel();

            var blocks = buildConfig.Blocks.AllBlocks;
            for (int i = 0; i < blocks.Length; i++)
            {
                GameObject blockPrefab = blocks[i];
                BaseBlock blockScript = blockPrefab.GetComponent<BaseBlock>();
                GameObject cellObject = Instantiate(cellPrefab, cellParent);
                BlockCell cell = cellObject.GetComponent<BlockCell>();

                string blockName = blockScript.Name;
                Sprite blockSprite = blockScript.Sprite;
                bool isSelected = selectedBlock == blockName;
                string blockId = blockScript.ID;

                cell.Init(blockSprite,blockName, isSelected, delegate
                {
                    SelectBlock(blockId);
                    GameWindowsManager.Instance.CloseWindow();
                });
            }
        }


        void ClearPanel()
        {
            var childCount = cellParent.childCount;
            for (int i = 0; i < childCount; i++)
                Destroy(cellParent.GetChild(i).gameObject);
        }

        void SelectBlock(string id)
        {
            EventManager.OnBlockSelected?.Invoke(id);
        }
        
    }
}