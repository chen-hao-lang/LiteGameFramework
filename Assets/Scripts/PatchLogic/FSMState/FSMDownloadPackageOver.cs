using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMDownloadPackageOver : IState
{
    private StateMachine stateMachine;
    private IState to;

    public void Create(StateMachine _machine, IState _to = null)
    {
        stateMachine = _machine;
        to = _to;
    }

    public void OnEnter()
    {
        //TODO:
        stateMachine.SetState(to);
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}