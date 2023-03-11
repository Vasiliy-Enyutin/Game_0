using _Project.Scripts.Descriptors.Resources;
using _Project.Scripts.Resources;
using JetBrains.Annotations;
using Zenject;

namespace _Project.Scripts.PlayerLogic
{
    [UsedImplicitly]
    public class InventoryService
    {
        [Inject]
        private ItemDescriptorCollection _itemDescriptorCollection = null!;

        private readonly InventoryModel _inventoryModel = null!;

        public InventoryService()
        {
            _inventoryModel = new InventoryModel();
        }

        public bool TryCollectItem(ResourceItemType type, int quantity)
        {
            // TODO Проверка на свободное место в инвентаре
            _inventoryModel.AddItem(_itemDescriptorCollection.GetDescriptor(type), quantity);
            return true;
        }

        public bool TryRemoveItem(ResourceItemType type, int quantity)
        {
            ResourceItemDescriptor resourceItemDescriptor = _itemDescriptorCollection.GetDescriptor(type);
            if (_inventoryModel.Items[resourceItemDescriptor] <= quantity)
            {
                return false;
            }

            _inventoryModel.RemoveItem(resourceItemDescriptor, quantity);
            return true;
        }
    }
}