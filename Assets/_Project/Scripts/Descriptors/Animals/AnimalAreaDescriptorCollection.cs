using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.AI;
using UnityEngine;

namespace _Project.Scripts.Descriptors.Animals
{
    [CreateAssetMenu(fileName = "AnimalDescriptorCollection", menuName = "Descriptors/AnimalCollection", order = 0)]
    public class AnimalAreaDescriptorCollection : ScriptableObject
    {
        public List<AnimalAreaDescriptor> Descriptors = null!;
        
        public AnimalAreaDescriptor GetDescriptor(AnimalType animalType)
        {
            return Descriptors.First(descriptor => descriptor.AnimalType == animalType);
        }
    }
}