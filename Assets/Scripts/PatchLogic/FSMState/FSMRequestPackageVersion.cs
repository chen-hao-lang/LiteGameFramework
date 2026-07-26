using UnityEngine;

public class FSMRequestPackageVersion : IState
{
    private StateMachine stateMachine;

    public FSMRequestPackageVersion(StateMachine _machine)
    {
        stateMachine = _machine;
    }

    public void Tick()
    {

    }

    public void OnEnter()
    {
        var packageName = (string)stateMachine.GetBlackboardData("PackageName");
        YooAssetsLoad.Instance.RequestPackageVersion(
            _success: (version) =>
            {
                stateMachine.AddBlackboardData("PackageVersion", version);
            },
            _fail: (error) =>
            {
                Debug.LogError($"[FSMRequestPackageVersion] Request version failed: {error}");
                //TODO: 触发版本请求失败事件，可进入错误状态或重试
            },
            _packageName: packageName
        );
    }

    public void OnExit()
    {

    }
}