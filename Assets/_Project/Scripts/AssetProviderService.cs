using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
	[UsedImplicitly]
	public class AssetProviderService
	{
		[Inject]
		private DiContainer _diContainer = null!;

		public T CreateAsset<T>(Object prefab, Vector3 position) where T : MonoBehaviour
		{
			T instance = _diContainer.InstantiatePrefab(prefab).GetComponent<T>();
			instance.transform.position = position;
			Debug.Log(position);
			return instance;
		}
		
		public T CreateAsset<T>(Object prefab, Transform parentTransform) where T : MonoBehaviour
		{
			T instance = CreateAsset<T>(prefab, parentTransform.position);
			instance.transform.SetParent(parentTransform);
			return instance;
		} 
	}
}
