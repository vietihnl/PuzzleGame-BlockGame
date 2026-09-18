using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI bestScoreValueText;
    [SerializeField] private TextMeshProUGUI yourScoreValueText;

    private const string BestScoreKey="BestScore";

    public void ShowGameOver(int finalScore)
    {
        int bestScore = PlayerPrefs.GetInt(BestScoreKey,0);

        if(finalScore>bestScore)
        {
            bestScore=finalScore;
            PlayerPrefs.SetInt(BestScoreKey,bestScore);
            PlayerPrefs.Save();
        }

        bestScoreValueText.text=bestScore.ToString();
        yourScoreValueText.text=finalScore.ToString();

        gameOverPanel.SetActive(true);
    }

    public void OnContinueButtonPressed()
    {
        gameOverPanel.SetActive(false);
        GameManager.Instance.RestartGame();
    }
}
