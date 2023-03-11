using _Project.Scripts.Resources;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.PlayerLogic
{
    public class ItemCollector : MonoBehaviour
    {
        [Inject]
        private PlayerInputService _playerInputService = null!;
        [Inject]
        private InventoryService _inventoryService = null!;
        
        private bool _isCollecting;

        public ResourceItem? CurrentItemForCollecting { get; set; }

        private void OnEnable()
        {
            _playerInputService.OnInteract += TryStartCollecting;
        }

        private void OnDisable()
        {
            _playerInputService.OnInteract -= TryStartCollecting;
        }

        private void TryStartCollecting()
        {
            if (_isCollecting || CurrentItemForCollecting == null)
            {
                return;
            }

            if (_inventoryService.TryCollectItem(CurrentItemForCollecting.Type, CurrentItemForCollecting.Quantity))
            {
                StartCollecting();
            }
        }

        private void StartCollecting()
        {
            _isCollecting = true;
            // TODO Анимация
            Debug.Log($"Collect resource item. name={CurrentItemForCollecting?.name}");
            CurrentItemForCollecting.Collect();
            _isCollecting = false;
        }
    }
}