using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class BuildingsGrid : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int _gridSize = new Vector2Int(10, 10);
        [SerializeField]
        private GameObject _originPoint = null!;

        [Inject] 
        private AssetProviderService _assetProviderService = null!;
        
        private Building[,] _grid = null!;
        private Building? _flyingBuilding;
        private Camera _mainCamera = null!;
        private Plane _groundPlane;
    
        private void Awake()
        {
            _grid = new Building[_gridSize.x, _gridSize.y];
            _mainCamera = Camera.main;
            transform.localScale = new Vector3(0.1f * _gridSize.x, 1, 0.1f * _gridSize.y);
        }

        public void StartPlacingBuilding(Building buildingPrefab)
        {
            if (_flyingBuilding != null) 
            {
                Destroy(_flyingBuilding.gameObject);
            }

            _flyingBuilding = _assetProviderService.CreateAsset<Building>(buildingPrefab, transform.position);
        }

        private void Update()
        {
            if (_flyingBuilding == null) 
            {
                return;
            }
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!(Physics.Raycast(ray, out RaycastHit hit) && hit.transform.gameObject.layer == 10))
            {
                return;
            }
            GetPlacementWorldCoordinates(ray, hit.point, out int globalX, out int globalY);
        
            int localX = globalX - (int) _originPoint.transform.position.x;
            int localY = globalY - (int) _originPoint.transform.position.z;
            bool isPlaceAvailable = !(IsBuildingOutOfGrid(localX, localY) || IsPlaceTaken(localX, localY));
        
            _flyingBuilding.transform.position = new Vector3(globalX, 0, globalY);
            _flyingBuilding.SetTransparent(isPlaceAvailable);
        
            if (!isPlaceAvailable || !Input.GetMouseButtonDown(0))
            {
                return;
            }
            PlaceFlyingBuilding(localX, localY);
        }

        private void GetPlacementWorldCoordinates(Ray ray, Vector3 position, out int x, out int y)
        {
            x = (int) Mathf.Floor(position.x);
            y = (int) Mathf.Floor(position.z);
        }
    
        private void PlaceFlyingBuilding(int placeX, int placeY)
        {
            for (int x = 0; x < _flyingBuilding.Size.x; x++)
            {
                for (int y = 0; y < _flyingBuilding.Size.y; y++)
                {
                    _grid[placeX + x, placeY + y] = _flyingBuilding;
                }
            }
            _flyingBuilding.SetNormal();
            _flyingBuilding = null;
        }
    
        private bool IsBuildingOutOfGrid(int placeX, int placeY)
        {
            if (placeX < 0 || placeX > _gridSize.x - _flyingBuilding.Size.x)
            {
                return true;
            }
            return placeY < 0 || placeY > _gridSize.y - _flyingBuilding.Size.y;
        }
    
        private bool IsPlaceTaken(int placeX, int placeY)
        {
            for (int x = 0; x < _flyingBuilding.Size.x; x++)
            {
                for (int y = 0; y < _flyingBuilding.Size.y; y++)
                {
                    if (_grid[placeX + x, placeY + y] != null) 
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
