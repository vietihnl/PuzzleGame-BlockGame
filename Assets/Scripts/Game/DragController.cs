using UnityEngine;
using UnityEngine.InputSystem;

public class DragController : MonoBehaviour
{
    [SerializeField] private LayerMask pieceLayer;
    [SerializeField] private Vector3 dragOffsetAboveFinger = new Vector3(0, 1.5f, 0);

    private BlockPiece draggingPiece;
    private int draggingSlotIndex = -1;

    void Update()
    {
        if(GameManager.Instance!=null&&GameManager.Instance.IsGameOver) return;
        if (Pointer.current == null) return;

        Vector2 screenPos = Pointer.current.position.ReadValue();
        Vector3 worldPos = GetWorldPos(screenPos);

        if (Pointer.current.press.wasPressedThisFrame)
        {
            TryStartDrag(worldPos);
        }
        else if (Pointer.current.press.wasReleasedThisFrame)
        {
            TryDropPiece(worldPos);
        }
        else if (Pointer.current.press.isPressed && draggingPiece != null)
        {
            UpdateDragPosition(worldPos);
        }
    }

    private Vector3 GetWorldPos(Vector2 screenPos)
    {
        if (Camera.main == null) return Vector3.zero;
        Vector3 pos3D = new Vector3(screenPos.x, screenPos.y, -Camera.main.transform.position.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos3D);
        worldPos.z = 0;
        return worldPos;
    }

    private void TryStartDrag(Vector3 worldPos)
    {
        int mask = pieceLayer.value == 0 ? ~0 : pieceLayer.value;
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, mask);
        if (hit.collider == null) return;

        BlockPiece piece = hit.collider.GetComponentInParent<BlockPiece>();
        if (piece == null || piece.IsPlaced) return;

        draggingPiece = piece;
        draggingSlotIndex = PieceManager.Instance.GetSlotIndexOf(piece);

        piece.SetFullScale();
        UpdateDragPosition(worldPos);
        SoundManager.Instance.PlayDragSound();
    }

    private void UpdateDragPosition(Vector3 worldPos)
    {
        if (draggingPiece == null) return;
        Vector3 centerOffset = draggingPiece.GetShapeCenterOffset();
        draggingPiece.transform.position = worldPos + dragOffsetAboveFinger - centerOffset;

        // Tính ô lưới gần nhất và hiện preview
        if (GridManager.Instance != null)
        {
            Vector2Int gridPos = GridManager.Instance.GetGridPosition(draggingPiece.transform.position);
            bool isValid = draggingPiece.CanPlaceAt(gridPos);
            GridManager.Instance.ShowPreview(gridPos, draggingPiece.GetCells(), isValid);
        }
    }

    private void TryDropPiece(Vector3 worldPos)
    {
        if (draggingPiece == null) return;

        // Xóa preview khi thả khối gạch
        if (GridManager.Instance != null)
        {
            GridManager.Instance.ClearPreview();
        }

        Vector2Int gridPos = GridManager.Instance.GetGridPosition(draggingPiece.transform.position);

        if (draggingPiece.CanPlaceAt(gridPos))
        {
            draggingPiece.PlaceAt(gridPos);
            SoundManager.Instance.PlayPlaceSound();
            PieceManager.Instance.ClearSlot(draggingSlotIndex);
            
            int linesCleared = LineClearChecker.Instance.CheckAndClearLines();
            if (linesCleared > 0)
            {
                SoundManager.Instance.PlayClearLineSound();

                // Rung mạnh dần theo số dòng xóa được
                float shakeMagnitude = 0.05f + (linesCleared * 0.03f);
                CameraShake.Instance.Shake(0.2f, shakeMagnitude);

                ComboUI.Instance.ShowCombo(linesCleared);
            }
            ScoreManager.Instance.AddLineClearScore(linesCleared);

            GameManager.Instance.CheckGameOver(PieceManager.Instance.GetCurrentPieces());
        }
        else
        {
            SoundManager.Instance.PlayDropSound();
            PieceManager.Instance.ReturnPieceToSlot(draggingPiece, draggingSlotIndex);
        }

        draggingPiece = null;
        draggingSlotIndex = -1;
    }
}
