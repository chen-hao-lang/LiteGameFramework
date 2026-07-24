using YooAsset;

public class FSMClearCacheBundle : IState
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
        var packageName = stateMachine.GetBlackboardData("PackageName").ToString();
        var package = YooAssets.GetPackage(packageName);
        var options = new ClearCacheOptions(ClearCacheMethods.ClearUnusedBundleFiles);
        var operation = package.ClearCacheAsync(options);
        operation.Completed += Operation_Completed;
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }

    private void Operation_Completed(YooAsset.AsyncOperationBase obj)
    {
        stateMachine.SetState(to);
    }
}
