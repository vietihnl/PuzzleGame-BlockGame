using UnityEngine;

public class LineClearChecker : MonoBehaviour
{
    public static LineClearChecker Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public int CheckAndClearLines()
    {
        int width=GridManager.Instance.Width;
        int height = GridManager.Instance.Height;
        int linesCleared=0;

        for(int y=0; y<height; y++)
        {
            if(GridManager.Instance.IsRowFull(y))
            {
                GridManager.Instance.ClearRow(y);
                linesCleared++;
            }
        }

        for(int x=0;x<width;x++)
        {
            if(GridManager.Instance.IsColumnFull(x))
            {
                GridManager.Instance.ClearColumn(x);
                linesCleared++;
            }
        }
        return linesCleared;
    }
}
