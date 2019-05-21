using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlControlPC : MonoBehaviour
{

    [SerializeField] private float speed = 5;
    private Camera mainCamera;

    Vector3 target;
    Vector3 start, end;
    LineRenderer lineRenderer;
    void Start()
    {
        mainCamera = Camera.main;
        target = transform.position;
        end = transform.position;
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    void Update ()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    target = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
        //}

        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, target, step);

        
        if (Input.GetMouseButtonDown(0))
        {
            start = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
            lineRenderer.SetPosition(0, start);
        }
        if (Input.GetMouseButtonUp(0))
        {
            end = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
            lineRenderer.SetPosition(1, end);
        }

        transform.position = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime);
    }
}
