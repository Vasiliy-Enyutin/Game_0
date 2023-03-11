using _Project.Scripts.Descriptors;
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
		
		public GameObject Player { get; private set; } = null!;

		public GameObject CreatePlayer(Vector3 position)
		{
			GameObject player = _assetProviderService.CreateAsset<PlayerMovement>(_playerDescriptor.Prefab, position).gameObject;
			Player = player;
			return player;
		}
		
		public void ClearAll()
		{
			Object.Destroy(Player.gameObject);
		}
	}
}
