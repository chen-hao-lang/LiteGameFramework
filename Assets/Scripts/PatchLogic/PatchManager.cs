using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

public class PatchManager : SingletonMono<PatchManager>
{
    private StateMachine machine;

    public void Init(string _packageNam,EPlayMode _ePlayMode)
    {
        machine = new StateMachine();

        machine.AddBlackboardData("PackageName",_packageNam);
        machine.AddBlackboardData("PlayMode",_ePlayMode);

        //TODO:
    }

    // Update is called once per frame
    void Update()
    {
        if(machine != null)
        {
            machine.Tick();
        }
    }
}
