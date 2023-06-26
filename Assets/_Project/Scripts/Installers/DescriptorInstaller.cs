using _Project.Scripts.Descriptors;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
	[CreateAssetMenu(fileName = "Custom Installers", menuName = "Descriptor", order = 0)]
	public class DescriptorInstaller : ScriptableObjectInstaller
	{
		[SerializeField]
		private PlayerDescriptor _playerDescriptor = null!;
		[SerializeField]
		private LocationDescriptor _locationDescriptor = null!;
		[SerializeField]
		private LabyrinthDescriptor _labyrinthDescriptor = null!;
		[SerializeField] 
		private EnemyDescriptor enemyDescriptor;
		
		public override void InstallBindings()
		{
			Container.BindInstance(_playerDescriptor).AsSingle();
			Container.BindInstance(_locationDescriptor).AsSingle();
			Container.BindInstance(_labyrinthDescriptor).AsSingle();
			Container.BindInstance(enemyDescriptor).AsSingle();
		}
	}
}
