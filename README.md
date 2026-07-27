# 项目结构
Assets/Scripts/
├── Launch.cs                          # 游戏启动入口
├── Test.cs                            # 测试脚本
├── Applications/                      # 应用层
│   ├── StartPanel.cs
│   └── TestPanel01.cs
├── Audio/
│   └── AudioManager.cs                # 音频管理器
├── Base/
│   ├── Singleton.cs                   # 泛型单例基类
│   └── SingletonMono.cs               # MonoBehaviour单例基类
├── Config/
│   └── UIConfig.json                  # UI配置文件
├── Coroutine/
│   └── CoroutineManager.cs            # 协程管理器
├── Data/
│   └── PlayerPrefsManager.cs          # 数据存储管理
├── Event/
│   ├── EventManager.cs                # 事件管理器
│   └── IEvent.cs                      # 事件接口
├── PatchLogic/                        # 热更新逻辑
│   ├── PatchManager.cs                # 热更新状态机入口
│   ├── Event/
│   │   └── OnPatchFailed.cs           # 热更新失败事件
│   └── FSMState/
│       ├── FSMClearCacheBundle.cs      # 清理缓存状态
│       ├── FSMDownloadPackage.cs       # 下载资源状态
│       ├── FSMInitializePackage.cs     # 初始化包状态
│       ├── FSMPatchFailed.cs           # 失败状态
│       ├── FSMRequestPackageVersion.cs # 请求版本状态
│       ├── FSMStartGame.cs             # 开始游戏状态
│       └── FSMUpdatePackageMainFest.cs # 更新清单状态
├── Pool/
│   └── GameObjectPool.cs              # 对象池
├── StateMachine/
│   ├── IState.cs                      # 状态接口
│   └── StateMachine.cs                # 通用状态机
├── UI/
│   ├── BasePanel.cs                   # 面板基类
│   ├── EUILayer.cs                    # UI层级枚举
│   ├── EUIType.cs                     # UI类型枚举
│   ├── IPanel.cs                      # 面板接口
│   ├── UIConfig.cs                    # UI配置类
│   ├── UIConfigJson.cs                # UI配置数据结构
│   ├── UIManager.cs                   # UI管理器
│   ├── UIPanelController.cs           # UI面板控制器
│   ├── UIRoot.cs                      # UI根节点
│   ├── Editor/
│   │   └── UIConfigEditorWindow.cs    # UI配置编辑器窗口
│   └── UIAnim/
│       ├── ButtonTween.cs             # 按钮动画组件
│       └── ContainerTween.cs          # 容器入场动画组件
└── YooAssets/                         # YooAssets资源管理
    ├── RmoteServices.cs               # 远端服务
    ├── YooAssetsLoad.cs               # 资源加载器
    └── Events/
        ├── OnDownloadComplete.cs       # 下载完成事件
        ├── OnDownloadError.cs          # 下载错误事件
        └── OnDownloadStart.cs          # 下载开始事件
