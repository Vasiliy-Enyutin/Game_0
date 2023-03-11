using _Project.Scripts.Resources;
using UnityEngine;

namespace _Project.Scripts.Descriptors.Resources
{
    [CreateAssetMenu(fileName = "ResourceItemDescriptor", menuName = "Descriptors/ResourceItem", order = 0)]
    public class ResourceItemDescriptor : ScriptableObject
    {
        public ResourceItemType Type;
        public ResourceItem ResourceItemPrefab = null!;
        public int Quantity;
        public string Name = null!;
    }
}
