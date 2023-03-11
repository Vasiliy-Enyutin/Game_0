using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Resources;
using UnityEngine;

namespace _Project.Scripts.Descriptors.Resources
{
    [CreateAssetMenu(fileName = "ResourceItemDescriptorCollection", menuName = "Descriptors/ResourceItemCollection", order = 0)]
    public class ItemDescriptorCollection : ScriptableObject
    {
        public List<ResourceItemDescriptor> Descriptors = null!;

        public ResourceItemDescriptor GetDescriptor(ResourceItemType type)
        {
            return Descriptors.First(descriptor => descriptor.Type == type);
        }
    }
}
