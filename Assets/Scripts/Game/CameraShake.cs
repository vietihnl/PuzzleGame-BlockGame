using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Vector3 originalPos;
    private Coroutine shakeCoroutine;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        originalPos = transform.localPosition;
    }

    public void SetOriginalPos(Vector3 pos)
    {
        originalPos = pos;
    }

    public void Shake(float duration, float magnitude)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            transform.localPosition = originalPos;
        }
        else
        {
            originalPos = transform.localPosition;
        }
        shakeCoroutine = StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed=0f;

        while(elapsed<duration)
        {
            float x=Random.Range(-1f,1f)*magnitude;
            float y=Random.Range(-1f,1f)*magnitude;

            transform.localPosition=originalPos+new Vector3(x,y,0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition=originalPos;
        shakeCoroutine=null;
    }
}