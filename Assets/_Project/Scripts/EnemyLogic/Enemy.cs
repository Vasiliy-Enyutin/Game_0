using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.EnemyLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        private GameObject? _player;
        private float _pursuitDistance;

        private NavMeshAgent _agent;
        
        public void Init(GameObject player, float moveSpeed, float pursuitDistance)
        {
            _player = player;
            _pursuitDistance = pursuitDistance;
            
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = moveSpeed;
        }

        private void Update()
        {
            NavMeshPath path = new();
            if (_agent.CalculatePath(_player.transform.position, path))
            {
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    float distanceToPlayer = 0;
                    Vector3[] corners = path.corners;
                    for (int i = 0; i < corners.Length - 1; ++i)
                    {
                        distanceToPlayer += Vector3.Distance(corners[i], corners[i + 1]);
                    }

                    if (distanceToPlayer < _pursuitDistance)
                    {
                        _agent.SetPath(path);
                    }
                }
            }
        }
    }
}
