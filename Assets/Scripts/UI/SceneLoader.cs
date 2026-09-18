using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
