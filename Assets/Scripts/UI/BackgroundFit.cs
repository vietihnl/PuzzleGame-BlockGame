using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundFit : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;

    private SpriteRenderer sr;

    void Awake()
    {
        Fit();
    }

    void OnEnable()
    {
        Fit();
    }

    void Start()
    {
        Fit();
    }

    void Update()
    {
        Fit();
    }

    [ContextMenu("Fit Background")]
    public void Fit()
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null) return;
        if (targetCamera == null) targetCamera = Camera.main;
        if (targetCamera == null) return;

        float screenHeight = targetCamera.orthographicSize * 2f;
        float screenWidth = screenHeight * targetCamera.aspect;

        if (sr.drawMode == SpriteDrawMode.Sliced)
        {
            sr.size = new Vector2(Mathf.Max(11.6f, screenWidth), Mathf.Max(19.2f, screenHeight));
        }
        else
        {
            float spriteW = sr.sprite.rect.width / sr.sprite.pixelsPerUnit;
            float spriteH = sr.sprite.rect.height / sr.sprite.pixelsPerUnit;

            if (spriteW > 0 && spriteH > 0)
            {
                float sX = screenWidth / spriteW;
                float sY = screenHeight / spriteH;
                float scale = Mathf.Max(sX, sY, 1.05f);
                transform.localScale = new Vector3(scale, scale, 1f);
            }
        }
    }
}
