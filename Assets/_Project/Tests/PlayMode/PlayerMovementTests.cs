using System.Collections;
using _Project.Scripts.PlayerLogic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _Project.Tests.PlayMode
{
    public class PlayerMovementTests
    {
        private const float MOVE_SPEED = 5f;
        private const float DELTA_TIME = 0.0167f;
        
        private PlayerMovement _playerMovement;
        private Transform _playerGfxTransform;
        private Rigidbody _rigidbody;

        [SetUp]
        public void Setup()
        {
            _playerMovement = new GameObject().AddComponent<PlayerMovement>();
            _rigidbody = _playerMovement.gameObject.AddComponent<Rigidbody>();

            // Загружаем PlayerGFX из ресурсов
            GameObject playerGfxPrefab = Resources.Load<GameObject>("PlayerGFX");

            // Создаем экземпляр PlayerGFX и делаем его дочерним объектом playerObject
            _playerGfxTransform = Object.Instantiate(playerGfxPrefab, _playerMovement.gameObject.transform).transform;
        }

        [Test]
        public void Move_ChangesMoveDirectionAccordingToPlayerInput()
        {
            // Arrange
            Vector3 moveDirection  = new(0.5f, 0, 0.5f);

            // Act
            _playerMovement.ConstructTest(_playerGfxTransform, moveDirection, MOVE_SPEED, _rigidbody);

            // Assert
            Assert.AreEqual(new Vector3(0.5f, 0, 0.5f), _playerMovement.GetMoveDirectionTest());
        }

        [Test]
        public void Move_NormalizesMoveDirectionWhenMagnitudeIsGreaterThanOne()
        {
            // Arrange
            Vector3 moveDirection  = new(1f, 0f, 1f);
        
            // Act
            _playerMovement.ConstructTest(_playerGfxTransform, moveDirection, MOVE_SPEED, _rigidbody);
        
            // Assert
            Assert.AreEqual(moveDirection.normalized, _playerMovement.GetMoveDirectionTest());
        }
        
        [UnityTest]
        public IEnumerator Move_MovesPlayerAccordingToMoveDirection()
        {
            // Arrange
            Vector3 moveDirection = new(1f, 0f, 1f);
            Vector3 startPlayerPosition = _playerMovement.transform.position;

            // Act
            _playerMovement.ConstructTest(_playerGfxTransform, moveDirection, MOVE_SPEED, _rigidbody);
            yield return new WaitForFixedUpdate();

            // Assert
            Vector3 expectedPosition = startPlayerPosition + moveDirection.normalized * MOVE_SPEED * Time.fixedDeltaTime;
            Assert.AreEqual(expectedPosition.x, _playerMovement.transform.position.x);
            Assert.AreEqual(expectedPosition.z, _playerMovement.transform.position.z);
        }
        
        // [Test]
        // public void Move_MovesPlayerAccordingToMoveDirection()
        // {
        //     // Arrange
        //     Vector3 moveDirection  = new(1f, 0f, 1f);
        //
        //     // Act
        //     _playerMovement.ConstructTest(_playerGfxTransform, moveDirection, MOVE_SPEED, _rigidbody);
        //
        //     // Assert
        //     Vector3 expectedPosition = _playerMovement.transform.position + moveDirection.normalized * MOVE_SPEED * DELTA_TIME;
        //     Assert.AreEqual(expectedPosition, _playerMovement.transform.position);
        // }
        
        // [Test]
        // public void RotatePlayer_MoveDirectionNotZero_RotatesPlayerGfxTransform()
        // {
        //     // Arrange
        //     _playerMovement.SetMoveDirection(new Vector3(1, 0, 1));
        //
        //     // Act
        //     _playerMovement.RotatePlayer();
        //
        //     // Assert
        //     Quaternion expectedRotation = Quaternion.LookRotation(new Vector3(1, 0, 1));
        //     Assert.AreEqual(expectedRotation, _playerMovement.GetPlayerGfxTransformRotation());
        // }
        //
        // [Test]
        // public void RotatePlayer_MoveDirectionZero_DoesNotRotatePlayerGfxTransform()
        // {
        //     // Arrange
        //     _playerMovement.SetMoveDirection(Vector3.zero);
        //
        //     // Act
        //     _playerMovement.RotatePlayer();
        //
        //     // Assert
        //     Quaternion expectedRotation = Quaternion.identity;
        //     Assert.AreEqual(expectedRotation, _playerMovement.GetPlayerGfxTransformRotation());
        // }
    }
}
