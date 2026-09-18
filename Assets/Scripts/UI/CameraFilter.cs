using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class CameraFilter : MonoBehaviour
{
    [Header("Target World Size to Fit")]
    [Tooltip("Chiều rộng vùng hiển thị ")]
    [SerializeField] private float targetWidth = 11.6f;

    [Tooltip("Chiều cao vùng hiển thị")]
    [SerializeField] private float targetHeight = 19.5f;

    [Header("Camera Center Offset")]
    [Tooltip("Dịch tâm Camera theo trục Y nếu cần")]
    [SerializeField] private float centerYOffset = 0f;

    private Camera cam;

    void Awake()
    {
        FitCamera();
    }

    void OnEnable()
    {
        FitCamera();
    }

    void Start()
    {
        FitCamera();
    }

    void Update()
    {
        FitCamera();
    }

    void OnValidate()
    {
        FitCamera();
    }

    [ContextMenu("Fit Camera Now")]
    public void FitCamera()
    {
        if (cam == null) cam = GetComponent<Camera>();
        if (cam == null) return;

        // Căn vị trí tâm của Camera theo trục Y
        Vector3 pos = cam.transform.position;
        pos.y = centerYOffset;
        cam.transform.position = pos;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (screenHeight <= 0 || screenWidth <= 0) return;

        float screenAspect = screenWidth / screenHeight;

        // 1. Orthographic Size cần thiết theo chiều cao
        float orthoSizeByHeight = targetHeight / 2f;

        // 2. Orthographic Size cần thiết theo chiều rộng
        float orthoSizeByWidth = (targetWidth / 2f) / screenAspect;

        // Lấy giá trị lớn hơn để đảm bảo cả chiều rộng và chiều cao đều hiển thị trọn vẹn
        cam.orthographicSize = Mathf.Max(orthoSizeByHeight, orthoSizeByWidth);
    }
}
