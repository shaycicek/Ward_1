using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;
    float timer;

    // Update is called once per frame
    private void Start()
    {
        timer = 0;
        if(timer>0.5f)
        {
            currentState?.OnEnterState();
        }
        
    }
    void Update()
    {
        timer += Time.deltaTime;
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();
        if(nextState != null && nextState != currentState )
        {
            SwitchToTheNextState(nextState);
            //Switch to the next state
        }
    }

    private void SwitchToTheNextState(State nextState)
    {
        currentState.OnExitState();
        currentState = nextState;
        currentState.OnEnterState();
    }
}
