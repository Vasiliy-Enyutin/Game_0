using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    public class MouseLooker : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity;
        [SerializeField] private Camera _playerCamera;
        
        private float _xRotation = 0;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void LateUpdate()
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.fixedDeltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.fixedDeltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            _playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}