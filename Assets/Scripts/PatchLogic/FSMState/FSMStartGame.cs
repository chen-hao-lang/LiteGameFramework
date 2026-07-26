using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStartGame : IState
{
    private StateMachine stateMachine;

    public FSMStartGame(StateMachine _machine)
    {
        stateMachine = _machine;
    }

    public void OnEnter()
    {
        //TODO:
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}