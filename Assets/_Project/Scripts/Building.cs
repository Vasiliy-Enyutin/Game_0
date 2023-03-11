using UnityEngine;

namespace _Project.Scripts
{
    public class Building : MonoBehaviour
    {
        [SerializeField]
        private Renderer _mainRenderer = null!;
        [SerializeField]
        private Vector2Int _size = Vector2Int.one;
    
        private static readonly Color BrownColor = new Color(1f, 0.68f, 0f, 0.3f);
        private static readonly Color PurpleColor = new Color(0.88f, 0f, 1f, 0.3f);

        public void SetTransparent(bool available)
        {
            MeshColor = available ? Color.green : Color.red;
        }

        public void SetNormal()
        {
            MeshColor = Color.white;
        }

        private void OnDrawGizmos()
        {
            for (int x = 0; x < Size.x; x++)
            {
                for (int y = 0; y < Size.y; y++)
                {
                    Gizmos.color = (x + y) % 2 == 0 ? PurpleColor : BrownColor;
                    Gizmos.DrawCube(transform.position + new Vector3(x + 0.5f, 0, y + 0.5f), new Vector3(1, .1f, 1));
                }
            }
        }
    
        public Vector2Int Size
        {
            get { return _size; }
        }

        private Color MeshColor
        {
            set { _mainRenderer.material.color = value; }
        }
    }
}   

