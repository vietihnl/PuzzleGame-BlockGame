using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    [Header("Pause UI")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private string homeSceneName = "MainScreen";

    [Header("Audio UI (Optional)")]
    [SerializeField] private Image musicIcon;
    [SerializeField] private Image sfxIcon;
    [SerializeField] private TMP_Text musicStatusText;
    [SerializeField] private TMP_Text sfxStatusText;

    [Header("UI Visual Settings")]
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color mutedColor = new Color(1f, 1f, 1f, 0.4f);

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }

        UpdateAudioUI();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(homeSceneName);
    }

    public void ToggleMusic()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ToggleMusic();
        }
        UpdateAudioUI();
    }

    public void ToggleSFX()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ToggleSFX();
        }
        UpdateAudioUI();
    }

    private void UpdateAudioUI()
    {
        if (SoundManager.Instance == null) return;

        bool isMusicMuted = SoundManager.Instance.IsMusicMuted;
        bool isSFXMuted = SoundManager.Instance.IsSFXMuted;

        // Cập nhật text trạng thái nếu có gán
        if (musicStatusText != null)
        {
            musicStatusText.text = isMusicMuted ? "OFF" : "ON";
        }

        if (sfxStatusText != null)
        {
            sfxStatusText.text = isSFXMuted ? "OFF" : "ON";
        }

        // Cập nhật màu/độ mờ icon nếu có gán
        if (musicIcon != null)
        {
            musicIcon.color = isMusicMuted ? mutedColor : activeColor;
        }

        if (sfxIcon != null)
        {
            sfxIcon.color = isSFXMuted ? mutedColor : activeColor;
        }
    }

    void OnDestroy()
    {
        // Đảm bảo trả lại timeScale khi chuyển scene hoặc hủy object
        Time.timeScale = 1f;
    }
}
