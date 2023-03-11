using System;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    public class PlayerInputService : MonoBehaviour
    {
        public event Action? OnInteract;
        public event Action? onLeftButtonDown;
        
        private void Update()
        {
            if (Input.GetKeyDown("space")) {
                OnInteract?.Invoke();
            }

            if (Input.GetMouseButtonDown(0)) {
                onLeftButtonDown?.Invoke();
            }
        }

        public Vector3 MoveDirection
        {
            get { return new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")); }
        }
    }
}
