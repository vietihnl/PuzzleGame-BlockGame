using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private bool isGameOver=false;
    [SerializeField] private GameOverUI gameOverUI;

    public bool IsGameOver=>isGameOver;

    
    void Awake()
    {
        Instance=this;
    }

    public void CheckGameOver(BlockPiece[] currentPieces)
    {
        foreach(BlockPiece piece in currentPieces)
        {
            if(piece==null) continue; //bỏ qua slot trống

            if(piece.HasAnyValidPlacement())
            {
                return; //còn piece có thể đặt được
            }


        }
        TriggerGameOver();
    }
    public void TriggerGameOver()
    {
        isGameOver=true;
        SoundManager.Instance.PlayLoseSound();
        Debug.Log("Game Over");
        gameOverUI.ShowGameOver(ScoreManager.Instance.Score);
    }

    public void RestartGame()
    {
        isGameOver=false;

        GridManager.Instance.ClearEntireBoard();
        PieceManager.Instance.ClearAllPieces();
        PieceManager.Instance.SpawnNewTray();
        ScoreManager.Instance.ResetScore();
    }

    
}
