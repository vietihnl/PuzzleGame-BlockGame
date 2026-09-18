using UnityEngine;
using TMPro;
using System.Collections;

public class ComboUI : MonoBehaviour
{
    public static ComboUI Instance;
    [SerializeField] private GameObject comboRoot;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private float showDuration = 0.8f;

    private Coroutine hideCoroutine;

    void Awake()
    {
        Instance= this;
    }

    public void ShowCombo(int linesCleared)
    {
        string message= linesCleared switch
        {
            1 => "NICE!",
            2 => "GREAT!",
            3 => "COMBO X3!",
            _ => "AMAZING x{linesCleared}!"
        };

        comboText.text = message;
        comboRoot.SetActive(true);

        if(hideCoroutine !=null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(showDuration);
        comboRoot.SetActive(false);
    }
}
