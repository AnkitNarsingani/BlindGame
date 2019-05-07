using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlControl : MonoBehaviour
{
    [SerializeField] GirlStates currentState;
    private MemoryLeap memoryLeap;
    private GirlMovement movement;

	void Start ()
    {
        memoryLeap = GetComponent<MemoryLeap>();
        movement = GetComponent<GirlMovement>();
    }
	

	void Update ()
    {

	}

    public void ChangeStates()
    {
        switch(currentState)
        {
            case GirlStates.move:
                currentState = GirlStates.memorLeap;
                memoryLeap.enabled = true;
                movement.enabled = false;
                break;
            case GirlStates.memorLeap:
                currentState = GirlStates.move;
                memoryLeap.enabled = false;
                movement.enabled = true;
                break;
        }
    }
}

public enum GirlStates
{
    move,
    memorLeap
}