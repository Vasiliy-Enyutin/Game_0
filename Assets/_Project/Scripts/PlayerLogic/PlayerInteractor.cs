using JetBrains.Annotations;

namespace _Project.Scripts.PlayerLogic
{
    using UnityEngine;

    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] 
        private LayerMask _objectsToHit;
        [SerializeField] 
        private Camera _playerCamera;

        private const float INTERACT_DISTANT = 2f;
        private IInteractable interactable;

        private void Update()
        {
            Debug.DrawRay(_playerCamera.transform.position, _playerCamera.transform.forward * INTERACT_DISTANT, Color.yellow);

            interactable = TryDetectInteractableObject();
            if (interactable != null)
            {
                Debug.Log("Here is interactable");
            }
            Debug.Log("NO interactable");
        }

        [CanBeNull] 
        private IInteractable TryDetectInteractableObject()
        {
            if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out RaycastHit hit, INTERACT_DISTANT, _objectsToHit))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                return interactable;
                
                // if (interactable != null)
                // {
                //     HUD.Instance.ShowInteractHint();
                //     if (Input.GetMouseButtonDown(0))
                //         interactable.Interact();
                // }
                // else
                // {
                //     HUD.Instance.HideActiveHint();
                // }
            }
            // else
            // {
            //     HUD.Instance.HideActiveHint();
            // }
            return null;
        }
    }
}