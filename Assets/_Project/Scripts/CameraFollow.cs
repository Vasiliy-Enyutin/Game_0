using _Project.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [Inject]
        private GameFactoryService _gameFactoryService = null!;

        private GameObject _player = null!;

        private void Start()
        {
            _player = _gameFactoryService.Player;
        }
        
        private void LateUpdate()
        {
            transform.position = _player.transform.position;
        }
    }
}
