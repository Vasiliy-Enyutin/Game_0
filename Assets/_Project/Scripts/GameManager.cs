using _Project.Scripts.Factories;
using _Project.Scripts.PlayerLogic;
using _Project.Scripts.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;
using Menu = _Project.Scripts.UI.Panels.Menu;

namespace _Project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [Inject]
        private UiManager _uiManager;
        [Inject]
        private GameFactoryService _gameFactoryService;

        private void Start()
        {
            DisableCharactersMovement();
            
            _uiManager.OnUserReadyToPlay += StartGame;
            _uiManager.OnRestartKeyPressed += RestartLevel;
            
            _gameFactoryService.Player.GetComponent<Player>().OnDestroy += FinishLevel;
        }

        private void OnDisable()
        {
            _uiManager.OnUserReadyToPlay -= StartGame;
            _uiManager.OnRestartKeyPressed -= RestartLevel;

            if (_gameFactoryService.Player != null)
            {
                _gameFactoryService.Player.GetComponent<Player>().OnDestroy -= FinishLevel;
            }
        }

        private void StartGame()
        {
            _uiManager.HideAll();
            EnableCharactersMovement();
        }

        private void EnableCharactersMovement()
        {
            _gameFactoryService.Player.GetComponent<PlayerMovement>().enabled = true;
            _gameFactoryService.Enemies.ForEach(enemy => enemy.GetComponent<NavMeshAgent>().enabled = true);
        }

        private void DisableCharactersMovement()
        {
            _gameFactoryService.Player.GetComponent<PlayerMovement>().enabled = false;
            _gameFactoryService.Enemies.ForEach(enemy => enemy.GetComponent<NavMeshAgent>().enabled = false);
        }

        private void FinishLevel()
        {
            DisableCharactersMovement();
            _uiManager.ShowMenu(Menu.GameOver);
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
