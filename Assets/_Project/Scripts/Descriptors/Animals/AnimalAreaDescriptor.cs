using _Project.Scripts.AI;
using UnityEngine;

namespace _Project.Scripts.Descriptors.Animals
{
    [CreateAssetMenu(fileName = "AnimalDescriptor", menuName = "Descriptors/Animal", order = 0)]
    public class AnimalAreaDescriptor : ScriptableObject
    {
        public AnimalType AnimalType;
        public Animal AnimalPrefab = null!;
        public float WalkRadius;
        public float PositionsChangeDelay;
        public int AnimalsNumber;
    }
}