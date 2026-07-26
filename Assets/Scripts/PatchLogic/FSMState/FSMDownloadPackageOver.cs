using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMDownloadPackageOver : IState
{
    private StateMachine stateMachine;

    public FSMDownloadPackageOver(StateMachine _machine)
    {
        stateMachine = _machine;
    }

    public void OnEnter()
    {
        //TODO:
        // stateMachine.SetState(to);
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}