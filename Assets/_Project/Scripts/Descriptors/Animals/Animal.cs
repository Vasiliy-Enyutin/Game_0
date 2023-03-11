using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Descriptors.Animals
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Animal : MonoBehaviour
    {
        private NavMeshAgent _agent = null!;

        private float _walkRadius;
        private float _positionChangeDelay;

        public void Init(float walkRadius, float positionChangeDelay)
        {
            _walkRadius = walkRadius;
            _positionChangeDelay = positionChangeDelay;
            
            _agent = GetComponent<NavMeshAgent>();
            SetDestination();
        }

        private async void SetDestination()
        {
            while (gameObject != null)
            {
                Vector3 randomDirection = transform.position + Random.insideUnitSphere * _walkRadius;
                NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _walkRadius, NavMesh.AllAreas);
                _agent.destination = hit.position;
                
                await UniTask.Delay(TimeSpan.FromSeconds(_positionChangeDelay));
            }
        }
    }
}