using _Project.Scripts.AI;
using _Project.Scripts.Descriptors;
using _Project.Scripts.Descriptors.Animals;
using _Project.Scripts.Descriptors.Resources;
using _Project.Scripts.Factories;
using _Project.Scripts.Resources;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
	public class GameInitializer : MonoBehaviour
	{
		[Inject]
		private GameFactoryService _gameFactoryService = null!;
		[Inject]
		private LocationDescriptor _locationDescriptor = null!;
		[Inject] 
		private ResourceDescriptorCollection _resourceDescriptorCollection = null!;
		[Inject]
		private AnimalAreaDescriptorCollection _animalAreaDescriptorCollection = null!;

		private void Awake()
		{
			_gameFactoryService.CreatePlayer(_locationDescriptor.InitialPlayerPositionPoint);
			InitResources();
			InitAnimalAreas();
		}

		private void InitResources()
		{
			foreach (Resource resource in FindObjectsOfType<Resource>())
			{
				ResourceDescriptor descriptor = _resourceDescriptorCollection.GetDescriptor(resource.Type);
				resource.Init(descriptor.Hp, descriptor.resourceItemDescriptor);
			}
		}

		private void InitAnimalAreas()
		{
			foreach (AnimalArea animalArea in FindObjectsOfType<AnimalArea>())
			{
				AnimalAreaDescriptor areaDescriptor = _animalAreaDescriptorCollection.GetDescriptor(animalArea.AnimalType);
				animalArea.Init(areaDescriptor.AnimalPrefab, areaDescriptor.WalkRadius, areaDescriptor.PositionsChangeDelay, areaDescriptor.AnimalsNumber);
			}
		}
	}
}
