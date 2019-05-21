using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MemoryLeap : MonoBehaviour
{
    Camera mainCamera;
    public GameObject spriteMask;
    float timer;

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

