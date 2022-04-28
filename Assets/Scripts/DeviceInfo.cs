using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class DeviceInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetDeviceInfo();
        GetCharacterDeviceInfo();
        GetRoleDeviceInfo();
        GetNodeDeviceInfo();
        DeviceStateEvent();

        RightHand();
    }

    public void GetDeviceInfo()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log("全局设备:" + LogDeviceInfo(device));
        }
    }

    public void GetCharacterDeviceInfo()
    {
        /**
        * 设备特征描述了设备的功能或用途（例如，是否为头戴式）。InputDeviceCharacteristics 是一系列标志，可以添加到代码中，用于搜索符合特定规格的设备。可以按以下特征筛选设备：
设备	特征
HeadMounted	设备连接到用户的头部。它具有设备跟踪和眼球中心跟踪功能。此标志最常用于标识头戴式显示器 (HMD)。
Camera	设备具有摄像机跟踪功能。
HeldInHand	用户将设备握在手中。
HandTracking	设备代表物理跟踪的手。它具有设备跟踪功能，并且可能包含手和骨骼数据。
EyeTracking	设备可以执行眼球跟踪并具有 EyesData 功能。
TrackedDevice	可以在 3D 空间中跟踪设备。它具有设备跟踪功能。
Controller	设备具有按钮和轴的输入数据，并且可以用作控制器。
TrackingReference	设备代表静态跟踪参考对象。它具有设备跟踪功能，但该跟踪数据不应该更改。
Left	将此特征与 HeldInHand 或 HandTracking 特征组合使用，可以将设备标识为与左手关联。
Right	将此特征与 HeldInHand 或 HandTracking 特征组合使用，可以将设备标识为与右手关联。
Simulated6DOF	设备报告 6DOF 数据，但仅具有 3DOF 传感器。Unity 负责模拟位置数据。
底层 XR SDK 会报告这些特征。您可以使用 InputDevice.Characteristics 查找这些特征。设备可以并且通常应该具有多个特征，可以使用位标志来筛选和访问这些特征。
InputDevices.GetDevicesWithCharacteristics 提供了一种方法来搜索具有给定特征集的所有设备。例如，您可以使用以下代码搜索系统中可用的 Left、HeldInHand、Controller InputDevices：
                 */
        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

        foreach (var device in leftHandedControllers)
        {
            Debug.Log("左手特征检索:" + LogDeviceInfo(device));
        }
    }

    public void GetRoleDeviceInfo()
    {

        /*
         * 按角色访问输入设备
设备角色描述输入设备的一般功能。请使用 InputDeviceRole 枚举来指定设备角色。定义的角色有：

角色	描述
GameController	游戏主机风格的游戏控制器。
Generic	代表核心 XR 设备的设备，例如头戴式显示器或移动设备。
HardwareTracker	跟踪设备。
LeftHanded	与用户左手关联的设备。
RightHanded	与用户右手关联的设备。
TrackingReference	跟踪其他设备的设备，例如 Oculus 跟踪摄像机。
底层 XR SDK 会报告这些角色，但是不同的提供商可能会以不同的方式组织他们的设备角色。此外，用户可以换手，因此角色分配结果可能与用户握住输入设备的手不匹配。例如，用户必须将 Daydream 控制器设置为惯用右手或左手，但可以选择将控制器放在另一只手中。

GetDevicesWithRole 提供具有特定 InputDeviceRole 的所有设备的列表。例如，您可以使用 InputDeviceRole.GameController 获取任何已连接的 GameController 设备：
         */
        var gameControllers = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.GameController, gameControllers);

        foreach (var device in gameControllers)
        {
            Debug.Log("角色检索:" + LogDeviceInfo(device));
        }
    }


    public void GetNodeDeviceInfo()
    {
        /**
         * 按 XR 节点访问输入设备
XR 节点表示 XR 系统中的物理参考点（例如，用户的头部位置、他们的左右手或诸如 Oculus 摄像机之类的跟踪参考）。

XRNode 枚举定义了以下节点：

XR 节点	描述
CenterEye	用户的两个瞳孔之间的中点。
GameController	游戏主机风格的游戏控制器。您的应用程序可以有多个游戏控制器设备。
HardwareTracker	硬件跟踪设备，通常连接到用户或物理项。可以存在多个硬件跟踪器节点。
Head	由 XR 系统计算出的用户头部的中心点。
LeftEye	用户的左眼。
LeftHand	用户的左手。
RightEye	用户的右眼。
RightHand	用户的右手。
TrackingReference	跟踪参考点，例如 Oculus 摄像机。可以存在多个跟踪参考节点。
请使用 InputDevices.GetDevicesAtXRNode 来获取与特定 XRNode 关联的设备列表。下面的示例演示了如何获取惯用左手的控制器：
         */
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

        if (leftHandDevices.Count == 1)
        {
            UnityEngine.XR.InputDevice device = leftHandDevices[0];
            Debug.Log("左手节点检索:" + LogDeviceInfo(device));
        }
        else if (leftHandDevices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }
    }


    public void DeviceStateEvent()
    {
        /*
         * 监听设备连接和断开连接
输入设备在各帧之间是一致的，但是可以随时连接或断开连接。为避免反复检查设备是否已连接到平台，请使用 InputDevices.deviceConnected 和 InputDevices.deviceDisconnected 在设备连接或断开连接时通知您的应用程序。这些还为您提供了有关新连接的输入设备的参考。

由于您可以在多个帧上保留这些引用，因此设备可能会断开连接，或者不再可用。要检查设备的输入是否仍然可用，请使用 InputDevice.isValid。访问输入设备的脚本在尝试使用该设备之前，应在每个帧的开头进行此检查。
         */
        InputDevices.deviceConnected += (InputDevice device) =>
        {
            Debug.Log(device.isValid + "设备连接事件:" + LogDeviceInfo(device));
        };
        InputDevices.deviceDisconnected += (InputDevice device) =>
        {
            Debug.Log(device.isValid + "设备断开事件:" + LogDeviceInfo(device));
        };
    }




    protected InputDevice rightHand;
    public void RightHand()
    {
        /*
         * 访问输入设备上的输入功能
您可以从特定的 InputDevice 读取输入功能，例如扳机键按钮的状态。例如，要读取右扳机键的状态，请按照下列步骤操作：

1.使用 InputDeviceRole.RightHanded 或 XRNode.RightHand 获取惯用右手设备的实例。 2.有了正确的设备后，请使用 InputDevice.TryGetFeatureValue 方法访问当前状态。

TryGetFeatureValue() 尝试访问功能的当前值，并根据情况返回不同的值：

如果成功获取指定的功能值，则返回 true
如果当前设备不支持指定的功能，或者该设备无效（即控制器不再处于激活状态），则返回 false
要获取特定的按钮、触摸输入或游戏杆轴值，请使用 CommonUsages 类。CommonUsages 包括 XR 输入映射表中的每个 InputFeatureUsage，以及诸如位置和旋转之类的跟踪功能。以下代码示例使用 CommonUsages.triggerButton 来检测用户当前是否在特定 InputDevice 实例上按下扳机键按钮：
         */
        var devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, devices);

        if (devices.Count == 1)
        {
            rightHand = devices[0];
            Debug.Log("右手定位:" + LogDeviceInfo(rightHand));
        }
        else if (devices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// PICO 按键隐射图 https://developer.pico-interactive.com/docs/cn/12050/225281/
    /// Unity XR 按键值 https://docs.unity3d.com/cn/2020.3/ScriptReference/XR.CommonUsages.html
    /// <param name="device"></param>
    public void HandWatch(InputDevice device)
    {
        if (!device.isValid)
        {
            Debug.Log("设备失去连接:" + LogDeviceInfo(device));
            return;
        }
        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool triggerButton) && triggerButton)
        {
            Debug.Log("triggerButton value:" + triggerButton);
        }

        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool primaryButton) && primaryButton)
        {
            Debug.Log("primaryButton value:" + primaryButton);
        }

        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out bool gripButton) && gripButton)
        {
            Debug.Log("gripButton value:" + gripButton);
        }
        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out bool secondaryButton) && secondaryButton)
        {
            Debug.Log("secondaryButton value:" + secondaryButton);
        }
    }
    public Dictionary<string, string> cacheFeature = new Dictionary<string, string>();
    public Dictionary<string, bool> cacheBoolValue = new Dictionary<string, bool>();
    public List<string> ignoreFeature = new List<string>() { "ControllerStatus", "IsTracked", "DevicePosition", "DeviceVelocity", "DeviceAngularAcceleration", "DeviceAngularVelocity", "DeviceAcceleration", "Primary2DAxis" };
    public new List<UnityEngine.XR.InputFeatureUsage> inputFeatures = new List<UnityEngine.XR.InputFeatureUsage>();
    public void InputFeatureUsageWatch(InputDevice device)
    {
        device.StopHaptics();
        //["Primary2DAxis","MenuButton","Primary2DAxisClick","Trigger","TriggerButton","BatteryLevel","GripButton","PrimaryButton","SecondaryButton","PrimaryTouch","SecondaryTouch","Grip","Primary2DAxisTouch","TriggerTouch","ThumbRestTouch","ControllerStatus","IsTracked","TrackingState","DevicePosition","DeviceRotation","DeviceVelocity","DeviceAngularVelocity","DeviceAcceleration","DeviceAngularAcceleration"]

        //[{"Key":"Primary2DAxis","Value":"Vector2"},{"Key":"MenuButton","Value":"Boolean"},{"Key":"Primary2DAxisClick","Value":"Boolean"},{"Key":"Trigger","Value":"Single"},{"Key":"TriggerButton","Value":"Boolean"},{"Key":"BatteryLevel","Value":"Single"},{"Key":"GripButton","Value":"Boolean"},{"Key":"PrimaryButton","Value":"Boolean"},{"Key":"SecondaryButton","Value":"Boolean"},{"Key":"PrimaryTouch","Value":"Boolean"},{"Key":"SecondaryTouch","Value":"Boolean"},{"Key":"Grip","Value":"Single"},{"Key":"Primary2DAxisTouch","Value":"Boolean"},{"Key":"TriggerTouch","Value":"Boolean"},{"Key":"ThumbRestTouch","Value":"Boolean"},{"Key":"ControllerStatus","Value":"Boolean"},{"Key":"IsTracked","Value":"Boolean"},{"Key":"TrackingState","Value":"UInt32"},{"Key":"DevicePosition","Value":"Vector3"},{"Key":"DeviceRotation","Value":"Quaternion"},{"Key":"DeviceVelocity","Value":"Vector3"},{"Key":"DeviceAngularVelocity","Value":"Vector3"},{"Key":"DeviceAcceleration","Value":"Vector3"},{"Key":"DeviceAngularAcceleration","Value":"Vector3"}]
        var inputList = new List<string>();
        if (device.TryGetFeatureUsages(inputFeatures))
        {
            foreach (var feature in inputFeatures)
            {
                if (!cacheFeature.ContainsKey(feature.name))
                {
                    cacheFeature.Add(feature.name, feature.type.Name);
                    inputList.Add(feature.name);
                    Debug.Log($"设备特征列表:{feature.name} 类型:{feature.type} ");
                    Debug.Log($"设备特征列表 List:" + DynamicJson.Serialize(inputList));
                    Debug.Log($"设备特征列表 Map:" + DynamicJson.Serialize(cacheFeature));
                }
                if (ignoreFeature.Contains(feature.name))
                {
                    continue;
                }
                if (feature.type == typeof(Vector2))
                {
                    Vector2 featureValue;
                    if (device.TryGetFeatureValue(feature.As<Vector2>(), out featureValue))
                    {
                        Debug.Log(LogDeviceInfo(device) + string.Format(" 特征:{0} ,Vec2d值 :{1}", feature.name, featureValue.ToString()));
                    }
                }
                if (feature.type == typeof(Vector3))
                {
                    Vector3 featureValue;
                    if (device.TryGetFeatureValue(feature.As<Vector3>(), out featureValue))
                    {
                        Debug.Log(LogDeviceInfo(device) + string.Format(" 特征:{0} ,Vec3d值 :{1}", feature.name, featureValue.ToString()));
                    }
                }
                if (feature.type == typeof(bool))
                {
                    bool featureValue;
                    if (device.TryGetFeatureValue(feature.As<bool>(), out featureValue))
                    {
                        if (!cacheBoolValue.ContainsKey(feature.name))
                        {
                            Debug.Log(LogDeviceInfo(device) + string.Format(" 特征:{0} ,初始化 Bool值 :{1}", feature.name, featureValue.ToString()));
                            cacheBoolValue.Add(feature.name, featureValue);
                        }
                        else
                        {
                            if (cacheBoolValue.TryGetValue(feature.name, out bool oldValue) && oldValue != featureValue)
                            {
                                Debug.Log(LogDeviceInfo(device) + string.Format(" 特征:{0} ,更新Bool值 :{1}", feature.name, featureValue.ToString()));
                                cacheBoolValue[feature.name] = featureValue;
                            }
                        }

                    }
                }
            }
        }
    }


    protected string LogDeviceInfo(InputDevice device)
    {
        return string.Format("设备: '{0}' 角色: '{1}'", device.name, device.role.ToString());
    }
    public void Awake()
    {

    }
    // Update is called once per frame
    void Update()
    {
        HandWatch(rightHand);
        InputFeatureUsageWatch(rightHand);
    }
}
