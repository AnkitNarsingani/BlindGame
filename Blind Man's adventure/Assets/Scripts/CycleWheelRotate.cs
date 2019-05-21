using UnityEngine;
using UnityEngine.EventSystems;

public class CycleWheelRotate : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    RectTransform rect;
    Vector2 centerPoint;
    private float wheelPrevAngle = 0f;
    private float wheelAngle = 0f;
    private float wheelAngleCopy = 0f;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        GetCenterPoint();
    }

    private void Update()
    {
        rect.localEulerAngles = new Vector3(0, 0, -1) * wheelAngle;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 pointerPos = eventData.position;
        wheelPrevAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointerPos = eventData.position;

        float wheelNewAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);
        if (Vector2.Distance(pointerPos, centerPoint) > 20f)
        {
            if (pointerPos.x > centerPoint.x)
            {
                wheelAngleCopy += wheelNewAngle - wheelPrevAngle;
                if (wheelAngleCopy > wheelAngle)
                {
                    wheelAngle += wheelNewAngle - wheelPrevAngle;
                    wheelPrevAngle = wheelNewAngle;
                }     
                else
                    wheelAngleCopy = wheelAngle;
            }
            else
            {
                wheelAngleCopy -= wheelNewAngle - wheelPrevAngle;
                if (wheelAngleCopy > wheelAngle)
                {
                    wheelAngle -= wheelNewAngle - wheelPrevAngle;
                    wheelPrevAngle = wheelNewAngle;
                }      
                else
                    wheelAngleCopy = wheelAngle;
            }
                
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    private void GetCenterPoint()
    {
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);

        for (int i = 0; i < 4; i++)
        {
            corners[i] = RectTransformUtility.WorldToScreenPoint(null, corners[i]);
        }

        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];
        float width = topRight.x - bottomLeft.x;
        float height = topRight.y - bottomLeft.y;

        Rect _rect = new Rect(bottomLeft.x, topRight.y, width, height);
        centerPoint = new Vector2(_rect.x + _rect.width * 0.5f, _rect.y - _rect.height * 0.5f);
    }
}