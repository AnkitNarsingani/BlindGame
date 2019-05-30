using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MemoryLeap : MonoBehaviour
{
    Camera mainCamera;
    public GameObject spriteMask;
    float timer;

    private void OnEnable()
    {
        GirlControl.OnStateChanged += Reset;
    }

    private void OnDisable()
    {
        GirlControl.OnStateChanged -= Reset;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(!IsPointerOverUI())
        {
            timer += Time.deltaTime;
            if (timer > 0.03f)
            {
                if (Input.touchCount > 0)
                {
                    Instantiate(spriteMask, mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position), Quaternion.identity);
                }
                timer = 0;
            }
        }
    }

    private void Reset()
    {
        
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

