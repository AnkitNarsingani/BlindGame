using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [SerializeField] private SpriteRenderer backgroundSprite;
    [SerializeField] public float leftCameraBound, rightCameraBound;

    private void Awake()
    {
        float center = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)).x;
        float left = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float ratio = left - center;
        leftCameraBound = -(backgroundSprite.size.x / 2) - ratio;
        rightCameraBound = -leftCameraBound;
    }

    void LateUpdate()
    {
        if (transform.position.x >= leftCameraBound && transform.position.x <= rightCameraBound)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, leftCameraBound, rightCameraBound);
            transform.position = smoothedPosition;
        }
    }

}