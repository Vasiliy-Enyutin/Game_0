using _Project.Scripts.Factories;
using _Project.Scripts.PlayerLogic;
using _Project.Scripts.UI;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

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
        }

        private void OnDestroy()
        {
            _uiManager.OnUserReadyToPlay -= StartGame;
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
    }
}
