using _Project.Scripts.Descriptors.Animals;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.AI
{
    public class AnimalArea : MonoBehaviour
    {
        [SerializeField] 
        private AnimalType _animalType;
        
        [Inject]
        private AssetProviderService _assetProviderService = null!;
        
        private float _walkRadius;
        private float _positionsChangeDelay;
        private Animal _animalPrefab = null!;

        public void Init(Animal animalPrefab, float walkRadius, float positionsChangeDelay, int animalsNumber)
        {
            _animalPrefab = animalPrefab;
            _walkRadius = walkRadius;
            _positionsChangeDelay = positionsChangeDelay;
            
            SpawnAnimals(animalsNumber);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _walkRadius);
        }

        private void SpawnAnimals(int animalsNumber)
        {
            for (int i = 0; i < animalsNumber; i++)
            {
                _assetProviderService.CreateAsset<Animal>(_animalPrefab, transform).Init(transform.position, _walkRadius, _positionsChangeDelay);
            }
        }

        public AnimalType AnimalType
        {
            get
            {
                return _animalType;
            }
        }
    }
}
