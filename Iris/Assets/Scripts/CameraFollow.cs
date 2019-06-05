using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    [SerializeField] public float leftCameraBound, rightCameraBound;

    void LateUpdate()
    {
        if(transform.position.x >= leftCameraBound && transform.position.x <= rightCameraBound)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, leftCameraBound, rightCameraBound);
            transform.position = smoothedPosition;
        }
    }

}