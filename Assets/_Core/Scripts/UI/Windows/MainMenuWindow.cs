using System;
using _Core.Scripts.UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts
{
    public class MainMenuWindow : WindowBase
    {
        [SerializeField] private Button startGame_Button;
        [SerializeField] private Button loadGames_Button;
        [SerializeField] private Button settings_Button;
        [SerializeField] private Button exit_Button;



        void InitializeButtons()
        {
            //Set up interactable
            startGame_Button.interactable = true;
            loadGames_Button.interactable = false;
            settings_Button.interactable = false;
            exit_Button.interactable = true;
            
            //Set logic
            startGame_Button.onClick.RemoveAllListeners();
            startGame_Button.onClick.AddListener(delegate
            {
                WindowsManager.Instance.OpenWindow<StartGameWindow>();
            });
            
            exit_Button.onClick.RemoveAllListeners();
            exit_Button.onClick.AddListener(delegate
            {
                Application.Quit();
            });
        }

        protected override void OnOpen()
        {
            InitializeButtons();
        }

        protected override void OnClose()
        {
            
        }
    }
}