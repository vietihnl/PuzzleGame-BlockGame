using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width = 8;
    [SerializeField] private int height = 8;
    [SerializeField] private float cellSize = 1.25f;
    [SerializeField] private Vector3 origin = new Vector3(-5f, -4.375f, 0f); 

    [SerializeField]private bool[,] isFilled; //  lưu trạng thái
    private GameObject[,] cellVisuals;//lưu object hiển thị tại từng ô

    [Header("Preview Highlight")]
    [SerializeField] private GameObject highlightPrefab;
    [SerializeField] private Color validPreviewColor=new Color(0,1,0,0.4f);
    [SerializeField] private Color invalidPreviewColor= new Color(1,0,0,0.4f);

    [SerializeField] private GameObject clearEffectPrefab;

    private GameObject[] activeHighlights = new GameObject[0]; 


    public static GridManager Instance { get; private set; }
    public int Width => width;
    public int Height => height;
    public float CellSize => cellSize;
    void Awake()
    {
        Instance = this;
        isFilled = new bool[width, height];
        cellVisuals = new GameObject[width, height];
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return origin + new Vector3(x * cellSize, y * cellSize, 0);
    }

    public Vector2Int GetGridPosition(Vector3 worldPos)
    {
        Vector3 local = worldPos - origin;
        int x = Mathf.RoundToInt(local.x / cellSize);
        int y = Mathf.RoundToInt(local.y / cellSize);
        return new Vector2Int(x, y);
    }

   
    public Vector3 SnapToGrid(Vector3 worldPos)
    {
        Vector2Int gridPos = GetGridPosition(worldPos);
        return GetWorldPosition(gridPos.x, gridPos.y);
    }

    public bool IsInsideGrid(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public bool IsCellFree(int x, int y)
    {
        return IsInsideGrid(x, y) && !isFilled[x, y];
    }

    public void SetFilled(int x, int y, bool value, GameObject visual = null)
    {
        if (!IsInsideGrid(x, y)) return;
        isFilled[x, y] = value;
        cellVisuals[x, y] = value ? visual : null;
    }
    public bool IsRowFull(int y)
    {
        for (int x=0; x<width;x++)
        {
            if(!isFilled[x,y]) return false;
        }
        return true;
    }

    public bool IsColumnFull(int x)
    {
        for(int y=0; y<height; y++)
        {
            if(!isFilled[x,y]) return false;
        }
        return true;
    } 

    public void ClearRow(int y)
    {
        for(int x=0;x<width;x++)
        {
            if(cellVisuals[x,y]!=null)
            {
             SpawnClearEffect(GetWorldPosition(x,y));   
             Destroy(cellVisuals[x,y]);
            }
            isFilled[x,y]=false;
            cellVisuals[x,y]=null;

        }
    }

    public void ClearColumn(int x)
    {
        for(int y=0;y<height;y++)
        {
            if(cellVisuals[x,y]!=null)
            {
             SpawnClearEffect(GetWorldPosition(x,y));   
             Destroy(cellVisuals[x,y]);
            }
            isFilled[x,y]=false;
            cellVisuals[x,y]=null;

        }
    }

    public bool IsBoardEmpty()
    {
        for (int x=0; x<width; x++)
        {
            for(int y=0; y<height; y++)
            {
                if(isFilled[x,y]) return false;
            }
        }
        return true;
    }

    public void ClearEntireBoard()
    {
        for(int x=0;x<width;x++)
        {
            for(int y=0;y<height;y++)
            {
                if(cellVisuals[x,y]!=null)
                {
                    Destroy(cellVisuals[x,y]);
                }
                isFilled[x,y]=false;
                cellVisuals[x,y]=null;
            }
        }
    }

    public void ShowPreview(Vector2Int originCell, Vector2Int[] shapeCells, bool isValid)
    {
        ClearPreview();

        activeHighlights = new GameObject[shapeCells.Length];
        for (int i = 0; i < shapeCells.Length; i++)
        {
            Vector2Int gridPos = originCell + shapeCells[i];
            if (!IsInsideGrid(gridPos.x, gridPos.y)) continue;

            Vector3 worldPos = GetWorldPosition(gridPos.x, gridPos.y);
            GameObject highlight = Instantiate(highlightPrefab, worldPos, Quaternion.identity);

            SpriteRenderer sr = highlight.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = isValid ? validPreviewColor : invalidPreviewColor;
            }

            activeHighlights[i] = highlight;
        }
    }

    public void ClearPreview()
    {
        foreach(var h in activeHighlights)
        {
            if(h!=null) Destroy(h);
        }
        activeHighlights= new GameObject[0];
    }

    private void SpawnClearEffect(Vector3 pos)
    {
        if(clearEffectPrefab==null) return;
        Instantiate(clearEffectPrefab, pos, Quaternion.identity);
    }
}