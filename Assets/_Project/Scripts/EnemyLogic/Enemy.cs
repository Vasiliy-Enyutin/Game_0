using UnityEngine;

namespace _Project.Scripts.EnemyLogic
{
    public class Enemy : MonoBehaviour
    {
        private GameObject _player;
        private float _moveSpeed;
        private float _pursuitDistance;
        
        public void Init(GameObject player, float moveSpeed, float pursuitDistance)
        {
            _player = player;
            _moveSpeed = moveSpeed;
            _pursuitDistance = pursuitDistance;
        }

        private void Update()
        {
            if (Vector3.Distance(_player.transform.position, transform.position) < _pursuitDistance)
            {
                
            }
        }
    }
}
