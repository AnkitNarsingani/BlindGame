using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IrisMemoryLeapPC : MonoBehaviour
{
    Camera mainCamera;
    public GameObject spriteMask;
    float timer;

    private void OnEnable()
    {
        IrisControl.OnStateChanged += Reset;
    }

    private void OnDisable()
    {
        IrisControl.OnStateChanged -= Reset;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!IsPointerOverUI())
        {
            timer += Time.deltaTime;
            if (timer > 0.03f)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 spawnPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    spawnPosition.z = 10;
                    Instantiate(spriteMask, spawnPosition, Quaternion.identity);
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

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
}

