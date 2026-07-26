using YooAsset;

public class PatchManager : SingletonMono<PatchManager>
{
    private StateMachine machine;

    public void Init(string _packageNam, EPlayMode _ePlayMode)
    {
        machine = new StateMachine();

        machine.AddBlackboardData("PackageName", _packageNam);
        machine.AddBlackboardData("PlayMode", _ePlayMode);
        machine.AddBlackboardData("InitComplete",false);

        //TODO:
        FSMInitializePackage fSMInitializePackage = new FSMInitializePackage(machine);
        FSMRequestPackageVersion fSMRequestPackageVersion = new FSMRequestPackageVersion(machine);
        FSMUpdatePackageMainFest fSMUpdatePackageMainFest = new FSMUpdatePackageMainFest(machine);
        FSMCreateDownloader fSMCreateDownloader = new FSMCreateDownloader(machine);
        FSMDownloadPackageFiles fSMDownloadPackageFiles = new FSMDownloadPackageFiles(machine);
        FSMDownloadPackageOver fSMDownloadPackageOver = new FSMDownloadPackageOver(machine);
        FSMClearCacheBundle fSMClearCacheBundle = new FSMClearCacheBundle(machine);
        FSMStartGame fSMStartGame = new FSMStartGame(machine);

        machine.AddTransition(fSMInitializePackage, fSMRequestPackageVersion, () =>
            machine.GetBlackboardData("InitComplete") is true);
    }

    // Update is called once per frame
    void Update()
    {
        if (machine != null)
        {
            machine.Tick();
        }
    }
}
