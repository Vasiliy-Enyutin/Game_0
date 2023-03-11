using System.Collections.Generic;
using _Project.Scripts.Descriptors.Resources;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    public class InventoryModel
    {
        private readonly Dictionary<ResourceItemDescriptor, int> _items = new();

        public void AddItem(ResourceItemDescriptor resourceItem, int quantity)
        {
            if (_items.ContainsKey(resourceItem))
            {
                _items[resourceItem] += quantity;
            }
            else
            {
                _items[resourceItem] = quantity;
            }
        }

        public void RemoveItem(ResourceItemDescriptor resourceItem, int quantity)
        {
            if (_items[resourceItem] < quantity)
            {
                Debug.LogWarning($"Количество ресурса name={resourceItem.Name} в инвентаре меньше, чем вычитаемое количество");
            }
            
            _items[resourceItem] -= quantity;
            if (_items[resourceItem] <= 0)
            {
                _items.Remove(resourceItem);
            }
        }

        public Dictionary<ResourceItemDescriptor, int> Items { get { return _items; } }
    }
}