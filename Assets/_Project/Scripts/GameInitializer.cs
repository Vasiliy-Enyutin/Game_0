using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.AI;
using _Project.Scripts.Descriptors;
using _Project.Scripts.Descriptors.Animals;
using _Project.Scripts.Descriptors.Resources;
using _Project.Scripts.Factories;
using _Project.Scripts.LabyrinthLogic;
using _Project.Scripts.Resources;
using UnityEngine;
using Zenject;
using NavMeshBuilder = UnityEditor.AI.NavMeshBuilder;

namespace _Project.Scripts
{
	public class GameInitializer : MonoBehaviour
	{
		[Inject]
		private GameFactoryService _gameFactoryService = null!;
		[Inject] 
		private ResourceDescriptorCollection _resourceDescriptorCollection = null!;
		[Inject]
		private AnimalAreaDescriptorCollection _animalAreaDescriptorCollection = null!;
		[Inject]
		private LabyrinthDescriptor _labyrinthDescriptor = null!;


		private void Awake()
		{
			_gameFactoryService.CreatePlayer();
			InitResources();
			InitAnimalAreas();
			InitLabyrinth();
			SpawnEnemies();
			BuildNavMesh();
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

		private void InitLabyrinth()
		{
			FindObjectOfType<LabyrinthSpawner>().Init(_labyrinthDescriptor.CellPrefab, _labyrinthDescriptor.CellSize,
				_labyrinthDescriptor.LabyrinthWidth, _labyrinthDescriptor.LabyrinthHeight);
		}

		private void SpawnEnemies()
		{
			List<Vector3> cellsPositions = FindObjectsOfType<Cell>().Select(cell => cell.transform.position).ToList();
			_gameFactoryService.CreateEnemies(cellsPositions);
		}

		private void BuildNavMesh()
		{
			NavMeshBuilder.BuildNavMeshAsync();
		}
	}
}
