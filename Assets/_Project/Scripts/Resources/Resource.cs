using _Project.Scripts.Descriptors.Resources;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Resources
{
    [RequireComponent(typeof(Collider))]
    public class Resource : MonoBehaviour
    {
        [SerializeField] 
        private ResourceType _type;
        
        [Inject]
        private AssetProviderService _assetProviderService = null!;
        private const float ITEM_DROP_RADIUS = 1.5f;
        private Collider _collider;
        private float _currentHp;
        private ResourceItemDescriptor _resourceItemDescriptor = null!;

        public ResourceType Type { get { return _type; }}

        public void Init(float baseHp, ResourceItemDescriptor resourceItemDescriptor)
        {
            _currentHp = baseHp;
            _resourceItemDescriptor = resourceItemDescriptor;
        }
        
        public bool TryToDestroy(float damage)
        {
            _currentHp -= damage;
            Debug.Log($"Trying to destory object. name={gameObject.name}, currentHp={_currentHp}");
            PlayGetDamageAnim();

            if (_currentHp <= 0) {
                Die();
                return true;
            }

            return false;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void Die()
        {
            SpawnResourceItem();
            _collider.enabled = false;
            Destroy(gameObject);
        }

        private void SpawnResourceItem()
        {
            Vector3 randomPointInSphere = Random.insideUnitCircle * ITEM_DROP_RADIUS;
            Vector3 spawnPoint = transform.position + new Vector3(randomPointInSphere.x, 0, randomPointInSphere.z);
            ResourceItem resourceItem = _assetProviderService.CreateAsset<ResourceItem>(_resourceItemDescriptor.ResourceItemPrefab, spawnPoint).GetComponent<ResourceItem>();
            resourceItem.Init(_resourceItemDescriptor.Quantity);
        }

        private void PlayGetDamageAnim()
        {
            Vector3 newScale = transform.localScale;
            newScale -= new Vector3(0.05f, 0f, 0.05f);
            transform.DOScale(newScale, 0.3f);
        }
    }
}
