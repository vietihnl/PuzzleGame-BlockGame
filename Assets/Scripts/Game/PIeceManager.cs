using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public static PieceManager Instance;

    [SerializeField] private GameObject piecePrefab;

    
    [SerializeField] private Vector3[] slotPositions = new Vector3[3]
    {
        new Vector3(-2.2f, -6f, 0),
        new Vector3(0f,    -6f, 0),
        new Vector3(2.2f,  -6f, 0)
    };

    private BlockPiece[] currentPieces;

    void Awake()
    {
        Instance = this;
        currentPieces = new BlockPiece[3];
    }

    void Start()
    {
        SpawnNewTray();
    }

    public void SpawnNewTray()
    {
        for (int i = 0; i < slotPositions.Length; i++)
        {
            SpawnPieceAtSlot(i);
        }
    }

    private void SpawnPieceAtSlot(int slotIndex)
    {
        GameObject pieceObj = Instantiate(piecePrefab, slotPositions[slotIndex], Quaternion.identity);
        BlockPiece piece = pieceObj.GetComponent<BlockPiece>();

        BlockType randomType = GetRandomType();
        piece.SetType(randomType);

        // Căn giữa hình dạng thật vào đúng tâm slot, bù trừ lệch pivot và tỉ lệ thu nhỏ
        piece.SetTrayScale();
        Vector3 centerOffset = piece.GetShapeCenterOffset() * piece.TraySlotScale;
        piece.transform.position = slotPositions[slotIndex] - centerOffset;

        currentPieces[slotIndex] = piece;
    }

    private BlockType GetRandomType()
    {
        System.Array allTypes = System.Enum.GetValues(typeof(BlockType));
        int randomIndex = Random.Range(0, allTypes.Length);
        return (BlockType)allTypes.GetValue(randomIndex);
    }

    public int GetSlotIndexOf(BlockPiece piece){

        for(int i=0; i<currentPieces.Length;i++)
        {
            if(currentPieces[i]==piece) return i;
        }
        return -1;
    }

    public void ClearSlot(int slotIndex)
    {
        if(slotIndex<0) return;

        currentPieces[slotIndex]=null;

        bool allEmpty=true;
        foreach(var p in currentPieces)
        {
            if(p!=null){allEmpty=false;break;}
        }

        if(allEmpty)
        {
            SpawnNewTray();
        }
    }

    public void ReturnPieceToSlot(BlockPiece piece, int slotIndex)
    {
        if (piece == null || slotIndex < 0 || slotIndex >= slotPositions.Length) return;

        piece.SetTrayScale();
        Vector3 centerOffset = piece.GetShapeCenterOffset() * piece.TraySlotScale;
        piece.transform.position = slotPositions[slotIndex] - centerOffset;
    }

    public BlockPiece[] GetCurrentPieces()
    {
        return currentPieces;
    }

    public void ClearAllPieces()
    {
        for(int i=0;i<currentPieces.Length;i++)
        {
            if(currentPieces[i]!=null)
            {
                Destroy(currentPieces[i].gameObject);
                currentPieces[i]=null;
            }
        }
    }
}