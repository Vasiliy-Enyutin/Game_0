using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.EnemyLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator = null!;
        
        private NavMeshAgent _agent;
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _animator.Play(_agent.velocity == Vector3.zero ? "Idle" : "Run");
        }
    }
}
