using UnityEngine;

namespace _Project.Scripts.LabyrinthLogic
{
    public class LabyrinthSpawner : MonoBehaviour
    {
        public Cell CellPrefab;
        public Vector3 CellSize = new(1,1,0);
        public Labyrinth maze;

        private void Start()
        {
            LabyrinthGenerator generator = new();
            maze = generator.GenerateMaze();

            for (int x = 0; x < maze.cells.GetLength(0); x++)
            {
                for (int y = 0; y < maze.cells.GetLength(1); y++)
                {
                    Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity);

                    c.transform.SetParent(transform);
                    c.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                    c.WallBottom.SetActive(maze.cells[x, y].WallBottom);
                }
            }
        }
    }
}