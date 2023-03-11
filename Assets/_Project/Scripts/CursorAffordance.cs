using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent (typeof(CameraRaycaster))]
    public class CursorAffordance : MonoBehaviour
    {
        [SerializeField] 
        private Texture2D? _walkCursor;
        [SerializeField] 
        private Texture2D? _unknownCursor;
        [SerializeField]
        private Texture2D? _targetCursor;
        [SerializeField] 
        private Vector2 _cursorHotspot = new Vector2(96, 96);

        private CameraRaycaster _cameraRaycaster = null!;

        private void Awake()
        {
            _cameraRaycaster = GetComponent<CameraRaycaster>();
        }

        private void OnEnable()
        {
            _cameraRaycaster.OnLayerChange += OnOnLayerChanged;
        }

        private void OnDisable()
        {
            _cameraRaycaster.OnLayerChange -= OnOnLayerChanged;
        }

        private void OnOnLayerChanged(Layer newLayer)
        {
            switch(newLayer)
            {
                case Layer.Walkable:
                    Cursor.SetCursor(_walkCursor, _cursorHotspot, CursorMode.Auto);
                    break;
                case Layer.RaycastEndStop:
                    Cursor.SetCursor(_unknownCursor, _cursorHotspot, CursorMode.Auto);
                    break;
                case Layer.Enemy:
                    Cursor.SetCursor(_targetCursor, _cursorHotspot, CursorMode.Auto);
                    break;
                default:
                    Debug.LogError($"Trying to switch cursor but layer is invalid. layer={newLayer}");
                    return;
            }
        }
    }
}
