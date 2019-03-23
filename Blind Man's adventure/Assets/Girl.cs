using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour {


	void Start ()
    {
		
	}
	

	void Update ()
    {
        transform.position += new Vector3(5 * Time.deltaTime, 0, 0);
	}
}
