using UnityEngine;

public class CameraFilter : MonoBehaviour
{
    [SerializeField] private float targetWidth=8.75f;
    [SerializeField] private float padding=0.3f;

    void Awake()
    {
        FitCamera();
    }

    void FitCamera()
    {
        Camera cam= GetComponent<Camera>();
        float screenAspect = (float)Screen.width/(float)Screen.height;

        float desiredHalfWidth=(targetWidth/2f)+padding;
        float requiredOrthoSize = desiredHalfWidth/screenAspect;

        if(requiredOrthoSize>cam.orthographicSize)
        {
            cam.orthographicSize=requiredOrthoSize;
        }
    }
}
