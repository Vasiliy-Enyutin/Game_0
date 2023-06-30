using System;
using _Project.Scripts.Descriptors;
using _Project.Scripts.UI.Panels;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI
{
    public class UiManager : MonoBehaviour
    {
        [Inject]
        private AssetProviderService _assetProviderService = null!;
        [Inject]
        private UiDescriptor _uiDescriptor = null!;

        public event Action OnUserReadyToPlay;

        private MainMenuPanel _mainMenuPanel;
        // private LevelWinPanel _levelWinPanel;
        // private GameOverPanel _gameOverPanel;

        private void Awake()
        {
            _mainMenuPanel = _assetProviderService.CreateAsset<MainMenuPanel>(_uiDescriptor.MainMenuPanelPrefab, transform);
            // _levelWinPanel = _assetProviderService.CreateAsset<MainMenuPanel>(_uiDescriptor.LevelWinPanelPrefab, transform);
            // _gameOverPanel = _assetProviderService.CreateAsset<MainMenuPanel>(_uiDescriptor.GameOverPanelPrefab, transform);

            _mainMenuPanel.OnPlayerAnyKeyDown += InvokeUserReadyToPlay;

            HideAll();
            ShowMenu(Menu.Main);
        }

        private void OnDestroy()
        {
            _mainMenuPanel.OnPlayerAnyKeyDown -= InvokeUserReadyToPlay;
        }

        public void ShowMenu(Menu menu)
        {
            HideAll();
            
            if (menu == Menu.Main)
            {
                _mainMenuPanel.Show();
            }
            // else if (menu == Menu.Win)
            // {
            //     _levelWinPanel.Show();
            // }
            // else if (menu == Menu.GameOver)
            // {
            //     _gameOverPanel.Show();
            // }
        }

        public void HideAll()
        {
            _mainMenuPanel.Hide();;
            // _levelWinPanel.Hide();;
            // _gameOverPanel.Hide();;
        }

        private void InvokeUserReadyToPlay()
        {
            OnUserReadyToPlay?.Invoke();
        }
    }
}
