using UnityEngine;
using Zenject;

namespace _Project.Scripts.LabyrinthLogic
{
    public class LabyrinthSpawner : MonoBehaviour
    {
        [Inject]
        private AssetProviderService _assetProviderService = null!;
        
        private Cell _cell;
        private Vector3 _cellSize;
        private int _labyrinthWidth;
        private int _labyrinthHeight;
        
        private Labyrinth _labyrinth;

        public void Init(Cell cell, Vector3 cellSize, int labyrinthWidth, int labyrinthHeight)
        {
            _cell = cell;
            _cellSize = cellSize;
            _labyrinthWidth = labyrinthWidth;
            _labyrinthHeight = labyrinthHeight;
            
            GenerateLabyrinth();
        }
        
        private void GenerateLabyrinth()
        {
            LabyrinthGenerator generator = new();
            _labyrinth = generator.GenerateMaze(_labyrinthWidth, _labyrinthHeight);

            for (int x = 0; x < _labyrinth.cells.GetLength(0); x++)
            {
                for (int y = 0; y < _labyrinth.cells.GetLength(1); y++)
                {
                    Cell c = _assetProviderService.CreateAsset<Cell>(_cell, new Vector3(x * _cellSize.x, y * _cellSize.y, y * _cellSize.z));

                    c.transform.SetParent(transform);
                    c.WallLeft.SetActive(_labyrinth.cells[x, y].WallLeft);
                    c.WallBottom.SetActive(_labyrinth.cells[x, y].WallBottom);
                }
            }
        }
    }
}