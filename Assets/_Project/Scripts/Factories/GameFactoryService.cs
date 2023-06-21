using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Descriptors;
using _Project.Scripts.EnemyLogic;
using _Project.Scripts.PlayerLogic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Factories
{
	[UsedImplicitly]
	public class GameFactoryService
	{
		[Inject]
		private AssetProviderService _assetProviderService = null!;
		[Inject]
		private PlayerDescriptor _playerDescriptor = null!;
		[Inject]
		private LocationDescriptor _locationDescriptor = null!;
		[Inject]
		private EnemyDescriptor _enemyDescriptor;

		// Из-за своеобразного спавна лабиринта пол ячеек может выходить за пределы лабиринта
		private const int CELLS_COORDS_OUTSIDE_LABYRINTH = 45;

		private GameObject _player;
		private List<GameObject> _enemies = new();
		
		public void CreatePlayer()
		{
			_player = _assetProviderService.CreateAsset<PlayerMovement>(_playerDescriptor.Prefab, _locationDescriptor.InitialPlayerPositionPoint).gameObject;
		}

		public void CreateEnemies(List<Vector3> cellsPositions)
		{
			List<Vector3> spawnPositions = cellsPositions
				.Where(x => Math.Abs(x.x - CELLS_COORDS_OUTSIDE_LABYRINTH) > Mathf.Epsilon &&
				            Math.Abs(x.z - CELLS_COORDS_OUTSIDE_LABYRINTH) > Mathf.Epsilon && (x.x + x.y != 0))
				.OrderBy(x => Guid.NewGuid()).Take(_enemyDescriptor.EnemiesNumber).ToList();

			
			foreach (Vector3 spawnPosition in spawnPositions)
			{
				Enemy enemy = _assetProviderService.CreateAsset<Enemy>(_enemyDescriptor.Enemy, spawnPosition);
				enemy.Init(_player, _enemyDescriptor.MoveSpeed, _enemyDescriptor.PursuitDistance);
				_enemies.Add(enemy.gameObject);
			}
		}
		
		public void ClearAll()
		{
			Object.Destroy(_player.gameObject);
			_enemies.ForEach(enemy => Object.Destroy(enemy));
		}
	}
}
