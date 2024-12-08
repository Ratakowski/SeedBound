using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer worldBorder;

    private Camera cam;
    private float camHalfWidth, camHalfHeight;
    private float minX, maxX, minY, maxY;

    private void Start()
    {
        cam = Camera.main;

        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;

        Bounds borderBounds = worldBorder.bounds;

        minX = borderBounds.min.x;
        maxX = borderBounds.max.x;
        minY = borderBounds.min.y;
        maxY = borderBounds.max.y;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position;

       
        float clampedX = Mathf.Clamp(targetPosition.x, minX + camHalfWidth, maxX - camHalfWidth);
        float clampedY = Mathf.Clamp(targetPosition.y, minY + camHalfHeight, maxY - camHalfHeight);

    
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
