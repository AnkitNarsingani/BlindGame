﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GirlMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private LayerMask nonInteractableLayer;
    bool isFlipped = false;

    public bool TargetReached { get; private set; }
    private Camera mainCamera;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                anim.SetBool("isWalking", false);
                OnTargetReached();
            }
        }
    }

    private void Move()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnTargetSet();
            target = new Vector3(mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position).x, transform.position.y, transform.position.z);     
        }
    }

    private void OnTargetReached()
    {
        TargetReached = true;
    }

    private void OnTargetSet()
    {
        TargetReached = false;
        CheckSpriteFlip();
    }

    void CheckSpriteFlip()
    {
        if (target.x < transform.position.x)
            spriteRenderer.flipX = true;
        else if (target.x > transform.position.x)
            spriteRenderer.flipX = false;
    }

    private void Reset()
    {
        target = transform.position;
        anim.SetBool("isWalking", false);
    }

    bool IsPointerOverUI()
    {
        if (Input.GetMouseButton(0))
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var go in raycastResults)
                {
                    if (go.gameObject.layer == nonInteractableLayer)
                        return true;
                }
            }
        }

        return false;
    }
}
