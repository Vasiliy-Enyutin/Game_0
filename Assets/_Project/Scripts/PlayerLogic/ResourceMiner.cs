using _Project.Scripts.Descriptors;
using _Project.Scripts.Resources;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.PlayerLogic
{
    public class ResourceMiner : MonoBehaviour
    {
        [Inject]
        private PlayerInputService _playerInputService = null!;
        [Inject] 
        private PlayerDescriptor _playerDescriptor = null!;
        
        private bool _isMining;

        public Resource? CurrentResourceForMining { get; set; }

        private void OnEnable()
        {
            _playerInputService.OnInteract += TryStartMining;
        }

        private void OnDisable()
        {
            _playerInputService.OnInteract -= TryStartMining;
        }

        private void TryStartMining()
        {
            if (_isMining || CurrentResourceForMining == null) {
                return;
            }

            StartMining();
        }

        private void StartMining()
        {
            _isMining = true;
            // TODO Анимация
            Debug.Log("Mining hit");
            if (CurrentResourceForMining.TryToDestroy(_playerDescriptor.BaseDamageToResources)) {
                CurrentResourceForMining = null;
            }
            _isMining = false;
        }
    }
}
