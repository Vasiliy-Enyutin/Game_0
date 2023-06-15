using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Descriptors.Animals
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Animal : MonoBehaviour
    {
        private NavMeshAgent _agent = null!;

        private Vector3 _startPosition;
        private float _walkRadius;
        private float _positionChangeDelay;

        public void Init(Vector3 startPosition, float walkRadius, float positionChangeDelay)
        {
            _startPosition = startPosition;
            _walkRadius = walkRadius;
            _positionChangeDelay = positionChangeDelay;
            
            _agent = GetComponent<NavMeshAgent>();

            SetDestination();
        }

        private async void SetDestination()
        {
            while (gameObject != null)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    Vector3 randomDirection = _startPosition + Random.insideUnitSphere * _walkRadius;
                    NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _walkRadius, NavMesh.AllAreas);
                    _agent.destination = hit.position;
                }

                await UniTask.Yield();
            }
        }
    }
}