using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GirlMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private Camera mainCamera;
    private Animator anim;

    Vector3 target;

    private void OnEnable()
    {
        GirlControl.OnStateChanged += Reset;
    }

    private void OnDisable()
    {
        GirlControl.OnStateChanged -= Reset;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
        target = transform.position;
    }

    void Update()
    {
        if (!IsPointerOverUI())
        {
            Move();

            if (Vector3.Distance(transform.position, target) > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                anim.SetBool("isWalking", true);
                
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
                anim.SetBool("isWalking", false);
            }
        }
    }

    private void Move()
    {

        if (Input.touchCount > 0)
        {
            target = new Vector3(mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position).x, transform.position.y, transform.position.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            target = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
        }

        if (target.x < transform.position.x)
            GetComponent<SpriteRenderer>().flipX = true;
    }

    private void Reset()
    {
        target = transform.position;
        anim.SetBool("isWalking", false);
    }

    bool IsPointerOverUI()
    {
        foreach (Touch t in Input.touches)
        {
            if (EventSystem.current.IsPointerOverGameObject(t.fingerId))
            {
                return true;
            }
        }
        return false;
    }


}
