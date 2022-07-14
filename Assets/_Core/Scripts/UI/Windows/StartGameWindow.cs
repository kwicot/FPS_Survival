using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Core.Scripts.UI.MainMenu
{
    public class StartGameWindow : WindowBase
    {
        [SerializeField] private GameData gameData;

        [SerializeField] private TMP_Dropdown levelsDropDown;
        [SerializeField] private TMP_Text levelNameText;
        [SerializeField] private TMP_Text levelSizeText;
        [SerializeField] private Image levelPreviewImage;

        private LevelInfo selectedLevel;
        
        protected override void OnOpen()
        {
            ClearLevelDropDown();
            InitializePanel();
            
            levelsDropDown.SetValueWithoutNotify(0);
            OnSelectLevel(0);
        }

        private void ClearLevelDropDown()
        {
            levelsDropDown.options.Clear();
        }

        void InitializePanel()
        {
            var levels = gameData.LevelsInfo;
            foreach (var levelInfo in levels)
                levelsDropDown.options.Add(new TMP_Dropdown.OptionData(levelInfo.Name));
            
            levelsDropDown.onValueChanged.RemoveAllListeners();
            levelsDropDown.onValueChanged.AddListener(OnSelectLevel);
        }

        private void OnSelectLevel(int index)
        {
            selectedLevel = gameData.LevelsInfo[index];

            levelNameText.text = selectedLevel.Name;
            levelSizeText.text = selectedLevel.Size;
            levelPreviewImage.sprite = selectedLevel.Preview;
        }

        public void StartGame()
        {
            SceneManager.LoadScene(selectedLevel.SceneName);
        }


        protected override void OnClose()
        {
            
        }
    }

    
}