using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using wvr;


[RequireComponent(typeof(Text))]
public class DebugInfoEvent : MonoBehaviour {
    private Text textField;
    WaveVR_Controller.EDeviceType mainControllerType = WaveVR_Controller.EDeviceType.Dominant;
    private string logMsg;
    private Queue logMsgQueue = new Queue();

    void Awake()
    {
        textField = GetComponent<Text>();
    }

    void OnEnable() {
        //Application.logMessageReceived += HandleLog;
    }

    void OnDisable() {
        //Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type) {
        logMsg = logString;
    }

    // Update is called once per frame
    void Update () {
        //if (WaveVR_Controller.Input(mainControllerType).GetPress(WVR_InputId.WVR_InputId_Alias1_Trigger))
        //{
        //    textField.text = "Press on Trigger";
        //}
        //if (WaveVR_Controller.Input(mainControllerType).GetPress(WVR_InputId.WVR_InputId_Alias1_Touchpad))
        //{
        //    textField.text = "Press on Touchpad";
        //}
        textField.text = logMsg;
    }
}
