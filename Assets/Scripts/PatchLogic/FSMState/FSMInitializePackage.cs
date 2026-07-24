using System.Collections;
using UnityEngine;
using YooAsset;

public class FSMInitializePackage : IState
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
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }

    public IEnumerator InitPackage()
    {
        var playMode = (EPlayMode)stateMachine.GetBlackboardData("PlayMode");
        var packageName = (string)stateMachine.GetBlackboardData("PackageName");

        if (!YooAssets.TryGetPackage(packageName, out var package))
        {
            package = YooAssets.CreatePackage(packageName);
        }

        InitializePackageOperation initializationOperation = null;
        if (playMode == EPlayMode.EditorSimulateMode)
        {
            var buildResult = EditorSimulateBuildInvoker.Build(packageName, (int)EBundleType.VirtualAssetBundle);
            var packageRoot = buildResult.PackageRootDirectory;

            var fileSystemParams = FileSystemParameters.CreateDefaultEditorFileSystemParameters(packageRoot);
            var createParameters = new EditorSimulateModeOptions();
            createParameters.EditorFileSystemParameters.AddParameter(EFileSystemParameter.VirtualWebglMode, true);
            createParameters.EditorFileSystemParameters.AddParameter(EFileSystemParameter.VirtualDownloadMode, true);
            createParameters.EditorFileSystemParameters.AddParameter(EFileSystemParameter.VirtualDownloadSpeed, 1024 * 1000);
            createParameters.EditorFileSystemParameters.AddParameter(EFileSystemParameter.AsyncSimulateMinFrame, 5);
            createParameters.EditorFileSystemParameters.AddParameter(EFileSystemParameter.AsyncSimulateMaxFrame, 10);
            initializationOperation = package.InitializePackageAsync(createParameters);
        }

        if (playMode == EPlayMode.OfflinePlayMode)
        {
            var createParameters = new OfflinePlayModeOptions();
            createParameters.BuiltinFileSystemParameters = FileSystemParameters.CreateDefaultBuiltinFileSystemParameters();
            initializationOperation = package.InitializePackageAsync(createParameters);
        }

        if (playMode == EPlayMode.HostPlayMode)
        {
            string defaultHostServer = GetHostServerURL();
            string fallbackHostServer = GetHostServerURL();
            IRemoteService remoteService = new RmoteServices(defaultHostServer, fallbackHostServer);
            var createParameters = new HostPlayModeOptions();
            createParameters.BuiltinFileSystemParameters = FileSystemParameters.CreateDefaultBuiltinFileSystemParameters();
            createParameters.BuiltinFileSystemParameters.AddParameter(EFileSystemParameter.CopyBuiltinPackageManifest, true);
            createParameters.CacheFileSystemParameters = FileSystemParameters.CreateDefaultSandboxFileSystemParameters(remoteService);
            createParameters.CacheFileSystemParameters.AddParameter(EFileSystemParameter.DownloadMaxConcurrency, 5);
            createParameters.CacheFileSystemParameters.AddParameter(EFileSystemParameter.DownloadMaxRequestPerFrame, 1);
            createParameters.CacheFileSystemParameters.AddParameter(EFileSystemParameter.DownloadWatchdogTimeout, 10);
            initializationOperation = package.InitializePackageAsync(createParameters);
        }

        yield return initializationOperation;

        if (initializationOperation.Status == EOperationStatus.Succeeded)
        {
            stateMachine.SetState(to);
        }
        else
        {
            Debug.LogWarning($"{initializationOperation.Error}");
            //TODO:触发初始化失败事件
        }
    }

    //TODO:
    private string GetHostServerURL()
    {
        string hostServerIP = "https://127.0.0.1";
        string appVersion = "v1.0";

#if UNITY_EDITOR
        if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
            return $"{hostServerIP}/CDN/Android/{appVersion}";
        else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
            return $"{hostServerIP}/CDN/IPhone/{appVersion}";
        else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
            return $"{hostServerIP}/CDN/WebGL/{appVersion}";
        else
            return $"{hostServerIP}/CDN/PC/{appVersion}";
#else
        if (Application.platform == RuntimePlatform.Android)
            return $"{hostServerIP}/CDN/Android/{appVersion}";
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            return $"{hostServerIP}/CDN/IPhone/{appVersion}";
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
            return $"{hostServerIP}/CDN/WebGL/{appVersion}";
        else
            return $"{hostServerIP}/CDN/PC/{appVersion}";
#endif
    }
}
