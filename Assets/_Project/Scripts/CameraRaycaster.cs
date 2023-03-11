using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class CameraRaycaster : MonoBehaviour
    {
        [SerializeField] 
        private float distanceToBackground = 100f;
        
        public event Action<Layer> OnLayerChange = null!;
        private Camera _viewCamera = null!;
        private RaycastHit _raycastHit;
        private Layer _layerHit;
        
        private readonly Layer[] _layerPriorities = 
        {
            Layer.Enemy,
            Layer.Walkable
        };
        
        private void Start() 
        {
            _viewCamera = Camera.main;
        }

        private void Update()
        {
            // Ищет и возвращает приоритетный layer hit
            foreach (Layer layer in _layerPriorities)
            {
                RaycastHit? hit = RaycastForLayer(layer);
                if (!hit.HasValue)
                {
                    continue;
                }
                _raycastHit = hit.Value;
            
                if (_layerHit == layer)
                {
                    return;
                }
                _layerHit = layer;
                OnLayerChange?.Invoke(layer);
                return;
            }
            // В другом случае возвращает background hit
            _raycastHit.distance = distanceToBackground;
            _layerHit = Layer.RaycastEndStop;
        }

        private RaycastHit? RaycastForLayer(Layer layer)
        {
            int layerMask = 1 << (int)layer;
            Ray ray = _viewCamera.ScreenPointToRay(Input.mousePosition);

            bool hasHit = Physics.Raycast(ray, out RaycastHit hitInfo, distanceToBackground, layerMask);
            if (hasHit)
            {
                return hitInfo;
            }
            return null;
        }
    
        public RaycastHit hit
        {
            get { return _raycastHit; }
        }

        public Layer currentLayerHit
        {
            get { return _layerHit; }
        }
    }
}
