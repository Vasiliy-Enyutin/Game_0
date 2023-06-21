using _Project.Scripts.PlayerLogic;
using UnityEngine;

namespace _Project.Scripts.EnemyLogic
{
    public class EnemyCollisionDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement playerMovement))
            {
                Destroy(playerMovement.gameObject);
            }
        }
    }
}
