using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlControl : MonoBehaviour
{
    [SerializeField] GirlStates currentState;
    private MemoryLeap memoryLeap;
    private GirlMovement movement;
    public delegate void StateChangeAction();
    public static event StateChangeAction OnStateChanged;

	void Start ()
    {
        memoryLeap = GetComponent<MemoryLeap>();
        movement = GetComponent<GirlMovement>();
        currentState = GirlStates.Move;
    }
	

	void Update ()
    {

	}

    public void ChangeStates()
    {
        OnStateChanged.Invoke();
        switch(currentState)
        {
            case GirlStates.Move:
                currentState = GirlStates.MemoryLeap;
                memoryLeap.enabled = true;
                movement.enabled = false;
                break;
            case GirlStates.MemoryLeap:
                currentState = GirlStates.Move;
                memoryLeap.enabled = false;
                movement.enabled = true;
                break;
        }
    }
}

public enum GirlStates
{
    Move,
    MemoryLeap
}