using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using wvr;


[RequireComponent(typeof(CanvasGroup))]
public class HeadCanvas : MonoBehaviour {
    private GameObject FPS;
    private Text textField;
    private GameObject minimapCanvas;
    private GameObject minimapCameraObj;
    private Camera minimapCamera;

    WaveVR_Controller.EDeviceType mainControllerType = WaveVR_Controller.EDeviceType.Dominant;
    private CanvasGroup canvasGroup;
    private string logMsg;
    private Queue logMsgQueue = new Queue();

    private bool showMinimap;
    private Texture textureA;
    private Texture2D textureB;

    void Awake()
    {
        FPS = transform.GetChild(0).gameObject;
        textField = FPS.GetComponent<Text>();
        textField.text = "";
        minimapCanvas = transform.GetChild(1).gameObject;
        //minimapCanvas.SetActive(true);
        //minimapCameraObj = GameObject.Find("/Subject/WaveVR/head/Minimap Camera");
        //minimapCamera = minimapCameraObj.GetComponent<Camera>();
        //minimapCamera.enabled = true;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;

        showMinimap = false;
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
        //textField.text = minimapCamera.enabled.ToString();

        //Show minimap or not
        if (WaveVR_Controller.Input(mainControllerType).GetPress(WVR_InputId.WVR_InputId_Alias1_Menu)) {
            ShowCanvas();
            showMinimap = true;
        } else {
            HideCanvas();
            showMinimap = false;
        }
    }

    private void HideCanvas()
    {
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    private void ShowCanvas()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
