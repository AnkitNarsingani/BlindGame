using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GirlMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private Camera mainCamera;

    Vector3 target;

    void Start()
    {
        mainCamera = Camera.main;
        target = transform.position;

    }

    void Update()
    {
        if(!IsPointerOverUI())
        {
            if (Input.touchCount > 0)
            {
                target = new Vector3(mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position).x, transform.position.y, transform.position.z);
            }

            if (Input.GetMouseButton(0))
            {
                target = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
            }

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
        Vector3 start, end;

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                start = mainCamera.ScreenToWorldPoint(touch.position);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                end = mainCamera.ScreenToWorldPoint(touch.position);
                transform.position = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            end = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime);
        }
    }

    bool IsPointerOverUI()
    {
        foreach(Touch t in Input.touches)
        {
            if(EventSystem.current.IsPointerOverGameObject(t.fingerId))
            {
                return true;
            } 
        }
        return false;
    }
}
