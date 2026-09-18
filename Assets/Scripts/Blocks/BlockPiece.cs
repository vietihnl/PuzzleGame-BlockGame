using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct BlockPrefabMapping
{
    [SerializeField] private BlockType type;
    [SerializeField] private GameObject prefab;

    public BlockType Type => type;
    public GameObject Prefab => prefab;
}

public class BlockPiece : MonoBehaviour
{
    [Header("Block")]
    [SerializeField] private BlockType type;

    [Header("Prefab piece")]
    [SerializeField] private List<BlockPrefabMapping> prefabMappings;

    [SerializeField] [Range(0.5f, 2f)] private float visualScale = 0.9f;
    [SerializeField] private float traySlotScale = 0.45f;
    
    public BlockType Type => type;
    public float TraySlotScale => traySlotScale;
    public bool IsPlaced { get; private set; } = false;

    void Start()
    {
        if (transform.childCount == 0)
        {
            BuildPiece();
        }
    }

    private GameObject GetPrefabForType(BlockType t)
    {
        foreach (var mapping in prefabMappings)
        {
            if (mapping.Type == t) return mapping.Prefab;
        }
        return null;
    }

    private void BuildPiece()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        float cellSize = GridManager.Instance.CellSize;
        GameObject prefabToUse = GetPrefabForType(type);

        if (prefabToUse == null)
        {
            Debug.LogWarning($"Không tìm thấy prefab cho loại {type}");
            return;
        }

        Vector2Int[] cells = BlockShape.GetCells(type);

        foreach (Vector2Int cell in cells)
        {
            GameObject blockCell = Instantiate(prefabToUse, transform);
            blockCell.transform.localPosition = new Vector3(cell.x * cellSize, cell.y * cellSize, 0);
            blockCell.transform.localScale = Vector3.one * (cellSize * visualScale);

            //Gán sorting Layer
            SpriteRenderer sr=blockCell.GetComponent<SpriteRenderer>();
            if(sr!=null)
            {
                sr.sortingLayerName="TrayPieces";
            }
            // Đảm bảo mỗi ô con có Collider2D để người chơi có thể nhấp và kéo
            if (blockCell.GetComponent<Collider2D>() == null)
            {
                blockCell.AddComponent<BoxCollider2D>();
            }
        }
    }

    public void SetType(BlockType newType)
    {
        type = newType;
        IsPlaced = false;
        BuildPiece();
    }
    
    public Vector3 GetShapeCenterOffset()
    {
        Vector2Int[] cells = BlockShape.GetCells(type);
        float cellSize = GridManager.Instance.CellSize;

        int minX = int.MaxValue, maxX = int.MinValue;
        int minY = int.MaxValue, maxY = int.MinValue;

        foreach (Vector2Int cell in cells)
        {
            if (cell.x < minX) minX = cell.x;
            if (cell.x > maxX) maxX = cell.x;
            if (cell.y < minY) minY = cell.y;
            if (cell.y > maxY) maxY = cell.y;
        }

        float centerX = (minX + maxX) / 2f * cellSize;
        float centerY = (minY + maxY) / 2f * cellSize;

        return new Vector3(centerX, centerY, 0);
    }

    public void SetTrayScale()
    {
        transform.localScale = Vector3.one * traySlotScale;
    }

    public void SetFullScale()
    {
        transform.localScale = Vector3.one;
    }

    public Vector2Int[] GetCells()
    {
        return BlockShape.GetCells(type);
    }

    public bool CanPlaceAt(Vector2Int originCell)
    {
        Vector2Int[] cells = GetCells();
        foreach (Vector2Int cell in cells)
        {
            Vector2Int checkPos = originCell + cell;
            if (!GridManager.Instance.IsCellFree(checkPos.x, checkPos.y))
            {
                return false;
            }
        }
        return true;
    }

    // Hàm khớp block vào lưới và khóa cứng vị trí
    public void PlaceAt(Vector2Int originCell)
    {
        IsPlaced = true;

        Vector2Int[] cells = GetCells();
        float targetScale = GridManager.Instance.CellSize * visualScale;

        // Lưu danh sách con ra List trước khi SetParent(null)
        // để tránh lỗi Unity foreach Transform bị bỏ sót ô con khi đổi Parent
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        for (int i = 0; i < children.Count; i++)
        {
            Transform child = children[i];
            Vector2Int gridPos = originCell + cells[i];

            // 1. Tách block con khỏi cha
            child.SetParent(null);
            child.position = GridManager.Instance.GetWorldPosition(gridPos.x, gridPos.y);
            child.localScale = Vector3.one * targetScale;

            //Đổi sortinglayer
            SpriteRenderer sr=child.GetComponent<SpriteRenderer>();
            if(sr!=null) sr.sortingLayerName="BoardBlocks";

            // 2. Khóa Collider
            Collider2D col = child.GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

            // 3. Đánh dấu vào GridManager và lưu visual GameObject
            GridManager.Instance.SetFilled(gridPos.x, gridPos.y, true, child.gameObject);
        }


        // 4. Xóa đối tượng cha rỗng sau khi đã tách toàn bộ block con
        Destroy(gameObject);

    }

    public bool HasAnyValidPlacement()
    {
        int width= GridManager.Instance.Width;
        int height=GridManager.Instance.Height;

        for(int x=0;x<width;x++)
        {
            for(int y=0;y<height;y++)
            {
                if(CanPlaceAt(new Vector2Int(x,y)))
                {
                    return true;
                }
            }
        }
        return false;
    }


}