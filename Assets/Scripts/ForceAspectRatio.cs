using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ForceAspectRatio : MonoBehaviour
{
    public float targetAspectWidth = 16f;
    public float targetAspectHeight = 9f;

    void Start()
    {
        ResizeWindow();
    }

    private void OnValidate()
    {
        ResizeWindow();
    }

    private void ResizeWindow()
    {
        float targetAspect = targetAspectWidth / targetAspectHeight;
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = GetComponent<Camera>();

        if (scaleHeight < 1f)
        {
            // Letterbox (add black bars top and bottom)
            Rect rect = camera.rect;

            rect.width = 1f;
            rect.height = scaleHeight;
            rect.x = 0f;
            rect.y = (1f - scaleHeight) / 2f;

            camera.rect = rect;
        }
        else
        {
            // Pillarbox (add black bars left and right)
            float scaleWidth = 1f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1f;
            rect.x = (1f - scaleWidth) / 2f;
            rect.y = 0f;

            camera.rect = rect;
        }
    }
}
