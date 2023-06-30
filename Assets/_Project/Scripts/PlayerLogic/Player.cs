using System;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    public class Player : MonoBehaviour
    {
        public event Action OnDestroy;
        
        public void Die()
        {
            OnDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}
