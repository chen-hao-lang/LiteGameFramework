using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

public class FSMDownloadPackageFiles : IState
{
    private StateMachine stateMachine;

    public FSMDownloadPackageFiles(StateMachine _machine)
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

    private IEnumerator StartDownload()
    {
        var downloader = (ResourceDownloaderOperation)stateMachine.GetBlackboardData("Downloader");
        //TODO:
        // downloader.DownloadError +=
        // downloader.DownloadProgressChanged +=
        downloader.StartDownload();
        yield return downloader;

        if(downloader.Status != EOperationStatus.Succeeded)
        {
            yield break;
        }

        // stateMachine.SetState(to);
    }
}
