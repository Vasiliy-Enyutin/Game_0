using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Resources;
using UnityEngine;

namespace _Project.Scripts.Descriptors.Resources
{
    [CreateAssetMenu(fileName = "ResourceDescriptorCollection", menuName = "Descriptors/ResourceCollection", order = 0)]
    public class ResourceDescriptorCollection : ScriptableObject
    {
        public List<ResourceDescriptor> Descriptors = null!;

        public ResourceDescriptor GetDescriptor(ResourceType type)
        {
            return Descriptors.First(descriptor => descriptor.ResourceType == type);
        }
    }
}
