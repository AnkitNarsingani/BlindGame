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

    void Start()
    {
        memoryLeap = GetComponent<MemoryLeap>();
        movement = GetComponent<GirlMovement>();
        currentState = GirlStates.Move;
        ChangeScripts(true);
    }


    void Update()
    {

    }

    public void ChangeStates()
    {
        OnStateChanged.Invoke();

        switch (currentState)
        {
            case GirlStates.Move:
                currentState = GirlStates.MemoryLeap;
                ChangeScripts(true);
                break;
            case GirlStates.MemoryLeap:
                currentState = GirlStates.Move;
                ChangeScripts(false);
                break;
        }
    }

    void ChangeScripts(bool moveScript)
    {
#if UNITY_EDITOR
        GetComponent<MemoryLeapPC>().enabled = !moveScript;
        GetComponent<GirlMovementPC>().enabled = moveScript;
#elif UNITY_ANDROID
        memoryLeap.enabled = !moveScript;
        movement.enabled = moveScript;
#endif
    }
}

public enum GirlStates
{
    Move,
    MemoryLeap
}