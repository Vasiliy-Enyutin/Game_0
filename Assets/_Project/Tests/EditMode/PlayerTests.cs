using _Project.Scripts.PlayerLogic;
using NUnit.Framework;
using UnityEngine;

namespace _Project.Tests.EditMode
{
    public class PlayerTests
    {
        private Player _player;
        private bool _onReachedFinishCalled;
        private bool _onDestroyCalled;

        [SetUp]
        public void Setup()
        {
            // Создаем новый объект Player для каждого теста
            _player = new GameObject().AddComponent<Player>();
            _onReachedFinishCalled = false;
            _onDestroyCalled = false;
        }

        [TearDown]
        public void TearDown()
        {
            // Уничтожаем объект Player после каждого теста
            Object.DestroyImmediate(_player.gameObject);
        }

        [Test]
        public void Die_InvokeOnDestroyEvent_DestroyGameObject()
        {
            // Подписываемся на событие OnDestroy
            _player.OnDestroy += () => _onDestroyCalled = true;

            // Act
            _player.Die();

            // Assert
            Assert.IsTrue(_onDestroyCalled);
            Assert.IsNull(_player.gameObject);
        }

        [Test]
        public void CollisionWithFinish_InvokeOnReachedFinishEvent()
        {
            // Подписываемся на событие OnReachedFinish
            _player.OnReachedFinish += () => _onReachedFinishCalled = true;

            // Act
            _player.CollisionWithFinish();

            // Assert
            Assert.IsTrue(_onReachedFinishCalled);
        }
    }
}