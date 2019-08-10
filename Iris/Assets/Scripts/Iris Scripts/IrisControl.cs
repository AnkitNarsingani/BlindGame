using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisControl : MonoBehaviour
{
    [SerializeField] IrisStates currentState;
    private IrisMemoryLeap memoryLeap;
    private IrisMovement movement;
    public delegate void StateChangeAction();
    public static event StateChangeAction OnStateChanged;

    void Start()
    {
        memoryLeap = GetComponent<IrisMemoryLeap>();
        movement = GetComponent<IrisMovement>();
        currentState = IrisStates.Move;
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
            case IrisStates.Move:
                currentState = IrisStates.MemoryLeap;
                ChangeScripts(true);
                break;
            case IrisStates.MemoryLeap:
                currentState = IrisStates.Move;
                ChangeScripts(false);
                break;
        }
    }

    void ChangeScripts(bool moveScript)
    {
#if UNITY_EDITOR
        GetComponent<IrisMemoryLeapPC>().enabled = !moveScript;
        GetComponent<IrisMovementPC>().enabled = moveScript;
#elif UNITY_ANDROID
        memoryLeap.enabled = !moveScript;
        movement.enabled = moveScript;
#endif
    }
}

public enum IrisStates
{
    Move,
    MemoryLeap
}