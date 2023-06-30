using System;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private UiManager _uiManager;

        private void Awake()
        {
            //TODO Убирать движение персонажей
            
            _uiManager.OnUserReadyToPlay += StartGame;
        }

        private void OnDestroy()
        {
            _uiManager.OnUserReadyToPlay -= StartGame;
        }

        private void StartGame()
        {
            _uiManager.HideAll();
            //TODO Запускать движение персонажей
        }
    }
}
