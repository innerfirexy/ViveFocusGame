using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using wvr;


[RequireComponent(typeof(Text))]
public class DebugInfo_Event : MonoBehaviour {
    private Text textField;
    WaveVR_Controller.EDeviceType mainControllerType = WaveVR_Controller.EDeviceType.Dominant;


    void Awake()
    {
        textField = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (WaveVR_Controller.Input(mainControllerType).GetPress(WVR_InputId.WVR_InputId_Alias1_Trigger))
        {
            textField.text = "Press on Trigger";
        }
        if (WaveVR_Controller.Input(mainControllerType).GetPress(WVR_InputId.WVR_InputId_Alias1_Touchpad))
        {
            textField.text = "Press on Touchpad";
        }
    }
}
