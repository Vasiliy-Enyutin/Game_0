using _Project.Scripts.Resources;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    [RequireComponent(typeof(ResourceMiner))]
    [RequireComponent(typeof(ItemCollector))]
    public class PlayerCollisionDetector : MonoBehaviour
    {
        private ResourceMiner _resourceMiner = null!;
        private ItemCollector _itemCollector = null!;

        private void Awake()
        {
            _resourceMiner = GetComponent<ResourceMiner>();
            _itemCollector = GetComponent<ItemCollector>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out ResourceItem resourceItem))
            {
                _itemCollector.CurrentItemForCollecting = resourceItem;
            }
            else if (other.TryGetComponent(out Resource resource)) 
            {
                _resourceMiner.CurrentResourceForMining = resource;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ResourceItem resourceItem))
            {
                _itemCollector.CurrentItemForCollecting = null;
            }
            else if (other.TryGetComponent(out Resource resource)) 
            {
                _resourceMiner.CurrentResourceForMining = null;
            }
        }
    }
}
