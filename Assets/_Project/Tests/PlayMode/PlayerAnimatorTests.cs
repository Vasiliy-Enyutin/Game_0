using System.Collections;
using _Project.Scripts.PlayerLogic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _Project.Tests.PlayMode
{
    public class PlayerAnimatorTests
    {
        private PlayerAnimator _playerAnimator;
        private Animator _animator;

        [SetUp]
        public void Setup()
        {
            // Создаем новый GameObject для Player
            GameObject playerObject = new GameObject("Player");

            // Добавляем компонент PlayerAnimator и получаем ссылку на него
            _playerAnimator = playerObject.AddComponent<PlayerAnimator>();

            // Загружаем PlayerGFX из ресурсов
            GameObject playerGFXPrefab = Resources.Load<GameObject>("PlayerGFX");

            // Создаем экземпляр PlayerGFX и делаем его дочерним объектом playerObject
            GameObject playerGFXObject = Object.Instantiate(playerGFXPrefab, playerObject.transform);

            // Получаем компонент Animator из PlayerGFXObject
            _animator = playerGFXObject.GetComponent<Animator>();

            // Вызываем метод ConstructTest с передачей аниматора и вектора направления
            _playerAnimator.ConstructTest(_animator, Vector3.zero);
        }

        [TearDown]
        public void TearDown()
        {
            // Уничтожаем объект игрока после каждого теста
            if (_playerAnimator != null)
            {
                Object.DestroyImmediate(_playerAnimator.gameObject);
            }
        }

        [UnityTest]
        public IEnumerator UpdateAnimation_MoveDirectionZero_PlayIdleAnimation()
        {
            // Arrange
            Vector3 moveDirection = Vector3.zero;

            // Act
            _playerAnimator.ConstructTest(_animator, moveDirection);

            // Wait for the next frame
            yield return null;

            // Assert
            Assert.IsTrue(_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
        }

        [UnityTest]
        public IEnumerator UpdateAnimation_MoveDirectionNotZero_PlayRunAnimation()
        {
            // Arrange
            Vector3 moveDirection = Vector3.forward;

            // Act
            _playerAnimator.ConstructTest(_animator, moveDirection);

            // Wait for the next frame
            yield return null;

            // Assert
            Assert.IsTrue(_animator.GetCurrentAnimatorStateInfo(0).IsName("Run"));
        }
    }
}
