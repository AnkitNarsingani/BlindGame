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
        if(!EventSystem.current.IsPointerOverGameObject())
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
    }
}
