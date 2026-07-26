using System.Collections;
using YooAsset;

public class FSMUpdatePackageMainFest : IState
{
    private IState to;
    private StateMachine stateMachine;

    public FSMUpdatePackageMainFest(StateMachine _machine)
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

    private IEnumerator UpdateMainfest()
    {
        var packageName = (string)stateMachine.GetBlackboardData("PackageName");
        var packageVersion = (string)stateMachine.GetBlackboardData("PackageVersion");
        var package = YooAssets.GetPackage(packageName);
        var options = new LoadPackageManifestOptions(packageVersion, 60);
        var operation = package.LoadPackageManifestAsync(options);
        yield return operation;

        if (operation.Status == EOperationStatus.Succeeded)
        {
            stateMachine.SetState(to);
        }
        else
        {
            //TODO:
        }
    }
}