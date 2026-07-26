using UnityEngine;
using YooAsset;

public class FSMCreateDownloader : IState
{
    private StateMachine stateMachine;

    public FSMCreateDownloader(StateMachine _machine)
    {
        stateMachine = _machine;
    }

    public void OnEnter()
    {
        CreateDownloaded();
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }

    //TODO:
    private void CreateDownloaded()
    {
        var packageName = (string)stateMachine.GetBlackboardData("PackageName");
        var package = YooAssets.GetPackage(packageName);
        int downloadingMaxNum = 10;
        int failedTryAgain = 3;
        var options = new ResourceDownloaderOptions(downloadingMaxNum,failedTryAgain);
        var downloader = package.CreateResourceDownloader(options);
        stateMachine.AddBlackboardData("Downloaded",downloader);

        if(downloader.TotalDownloadCount == 0)
        {
            Debug.Log("没有需要下载的东西");
            // stateMachine.SetState(to);
        }
        else
        {
            int totalDownloadCount = downloader.TotalDownloadCount;
            long totalDownloadBytes = downloader.TotalDownloadBytes;
            //TODO:发射事件信息
        }
    }
}
