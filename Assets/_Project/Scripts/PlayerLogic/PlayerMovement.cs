using _Project.Scripts.Descriptors;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.PlayerLogic
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform _playerGfxTransform;
        
        [Inject]
        private PlayerInputService _playerInputService = null!;
        [Inject]
        private PlayerDescriptor _playerDescriptor = null!;

        private Rigidbody _rigidbody = null!;
        
        private Vector3 _moveDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            RotatePlayer();
            Move();
        }

        private void RotatePlayer()
        {
            if (_moveDirection.magnitude > 0)
            {
                _playerGfxTransform.rotation = Quaternion.LookRotation(_moveDirection);
            }
        }

        private void Move()
        {
            _moveDirection = transform.right * _playerInputService.MoveDirection.x +
                             transform.forward * _playerInputService.MoveDirection.z;

            if (_moveDirection.magnitude > 1)
            {
                _moveDirection.Normalize();
            }

            _rigidbody.MovePosition(transform.position + _moveDirection * _playerDescriptor.MoveSpeed * Time.deltaTime);
        }
    }
}
