using UnityEngine;
using Zenject;

namespace _Project.Scripts.PlayerLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator = null!;
        
        [Inject]
        private PlayerInputService _playerInputService = null!;

        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _animator.Play(_playerInputService.MoveDirection == Vector3.zero ? "Idle" : "Run");
        }
    }
}
