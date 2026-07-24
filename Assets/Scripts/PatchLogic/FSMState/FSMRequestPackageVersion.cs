using UnityEngine;

public class FSMRequestPackageVersion : IState
{
    private StateMachine stateMachine;
    private IState to;

    public void Create(StateMachine _machine, IState _to = null)
    {
        stateMachine = _machine;
        to = _to;
    }

    public void Tick()
    {

    }

    public void OnEnter()
    {
        var packageName = (string)stateMachine.GetBlackboardData("PackageName");
        YooAssetsLoad.Instance.RequestPackageVersion(
            onSuccess: (version) =>
            {
                stateMachine.AddBlackboardData("PackageVersion", version);
                stateMachine.SetState(to);
            },
            onError: (error) =>
            {
                Debug.LogError($"[FSMRequestPackageVersion] Request version failed: {error}");
                //TODO: 触发版本请求失败事件，可进入错误状态或重试
            },
            packageName: packageName
        );
    }

    public void OnExit()
    {

    }
}