using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpriteMask : MonoBehaviour
{
    [HideInInspector] public Vector3 minSize;

    void Start()
    {
        minSize = new Vector3(0.1f, 0.1f, 0.1f);
    }


    void Update()
    {
        
        if(transform.localScale.x > 2f)
        {
            transform.localScale =  Vector3.Lerp(transform.localScale, minSize, 1.5f * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
