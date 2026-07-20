using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    private Camera uiCamera;
    /// <summary>
    /// 各个层级物体的游戏对象
    /// </summary>
    private Dictionary<EUILayer,Transform> layerTransforms;

    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        layerTransforms = new Dictionary<EUILayer, Transform>();

        // 创建UICamera
        var camera = GameObject.Find("UICamera");
        if (camera == null)
        {
            camera = new GameObject("UICamera");
        }
        if (camera.GetComponent<Camera>() == null)
        {
            uiCamera = camera.AddComponent<Camera>();
        }
        else
        {
            uiCamera = camera.GetComponent<Camera>();
        }
        uiCamera.cullingMask = 1 << LayerMask.NameToLayer("UI");
        uiCamera.transform.SetParent(transform);
        uiCamera.orthographic = true;
        uiCamera.clearFlags = CameraClearFlags.Depth;

        // 遍历EUILayer枚举，创建对应的UI层级
        foreach (EUILayer layer in System.Enum.GetValues(typeof(EUILayer)))
        {
            GameObject layerGO = new GameObject(layer.ToString());
            layerGO.transform.SetParent(transform);
            layerGO.transform.localPosition = Vector3.zero;
            layerGO.transform.localScale = Vector3.one;

            Canvas canvas = layerGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = (int)layer;

            CanvasScaler canvasScaler = layerGO.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;

            layerGO.AddComponent<GraphicRaycaster>();

            if(!layerTransforms.ContainsKey(layer))
            {
                layerTransforms.Add(layer,layerGO.transform);
            }
        }
    }

    /// <summary>
    /// 获得各个UI层级的父对象
    /// </summary>
    /// <param name="_layer"></param>
    /// <returns></returns>
    public Transform GetLayersTransform(EUILayer _layer)
    {
        if(layerTransforms.ContainsKey(_layer))
        {
            return layerTransforms[_layer];
        }
        else
        {
            Debug.LogWarning($"不存在当前 {_layer} 的对象,返回Normal的对象");
            return GetLayersTransform(EUILayer.Normal);
        }
    }
}