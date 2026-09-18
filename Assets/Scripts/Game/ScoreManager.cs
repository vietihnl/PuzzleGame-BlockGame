using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{ 
    public static ScoreManager Instance;

    [SerializeField] private int score=0;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Cấu hình điểm")]
    [SerializeField] private int pointsPerLine=100;
    [SerializeField] private int comboBonus=500;
    [SerializeField] private int comboThreshold=3;
    [SerializeField] private int fullBoardBonus=5000;

    public int Score=>score;

    void Awake()
    {
        Instance=this;
    }


    void Start()
    {
        UpdateScoreUI();
    }

    public void AddLineClearScore(int linesCleared)
    {
        if(linesCleared <=0) return;

        int gained=linesCleared*pointsPerLine;

        if(linesCleared>=comboThreshold)
        {
            gained+=comboBonus;
        }

        score+=gained;

        if(GridManager.Instance.IsBoardEmpty())
        {
            score+=fullBoardBonus;
        }
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if(scoreText!=null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void ResetScore()
    {
        score=0;
        UpdateScoreUI();
    }
}
