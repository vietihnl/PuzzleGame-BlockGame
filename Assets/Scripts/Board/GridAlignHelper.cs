using UnityEngine;

public class GridAlignHelper : MonoBehaviour
{  
    public int width =8;
    public int height =8;
    public float cellSize=1.25f;

    public Vector3 origin;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int x = 0; x <= width; x++)
        {
            Vector3 from = origin + new Vector3(x * cellSize, 0, 0);
            Vector3 to = origin + new Vector3(x * cellSize, height * cellSize, 0);
            Gizmos.DrawLine(from, to);
        }
        for (int y = 0; y <= height; y++)
        {
            Vector3 from = origin + new Vector3(0, y * cellSize, 0);
            Vector3 to = origin + new Vector3(width * cellSize, y * cellSize, 0);
            Gizmos.DrawLine(from, to);
        }
    }
}
