using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using WaveVR_Log;
using wvr;

public class Bot : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerHoverHandler
{
    private const string LOG_TAG = "WaveVR_Bot";
    public bool isControllerFocus;
    WaveVR_Controller.EDeviceType mainControllerType = WaveVR_Controller.EDeviceType.Dominant;
    private GameObject m_Controller;


	// Use this for initialization
	void Start () {
        //Log.d(LOG_TAG, "Bot started");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData) {
        Log.d(LOG_TAG, "OnPointerEnter: " + eventData.enterEventCamera.gameObject);

        WaveVR_Controller.EDeviceType type = eventData.enterEventCamera.gameObject.GetComponent<WaveVR_PoseTrackerManager>().Type;
        GameObject target = eventData.enterEventCamera.gameObject;

        if (target.GetComponent<WaveVR_PoseTrackerManager>()) {
            if (type == WaveVR_Controller.EDeviceType.Dominant) {
                m_Controller = target;
                isControllerFocus = true;
            }
        }

        if (isControllerFocus) {
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
        }
    }

    public void OnPointerExit(PointerEventData eventDate) {

    }

    public void OnPointerHover(PointerEventData eventData) {
        Log.d(LOG_TAG, "OnPointerHover: " + eventData.enterEventCamera.gameObject);
    }
}
