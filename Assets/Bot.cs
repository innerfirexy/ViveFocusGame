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
    private CanvasGroup canvasGroup;


	// Use this for initialization
	void Start () {
        //Log.d(LOG_TAG, "Bot started");
        isControllerFocus = false;
        canvasGroup = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (isControllerFocus) {
        //    GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
        //}
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //Log.d(LOG_TAG, "OnPointerEnter: " + eventData.enterEventCamera.gameObject);
        isControllerFocus = true;
        ShowCanvas();
    }

    public void OnPointerExit(PointerEventData eventData) {
        //Log.d(LOG_TAG, "OnPointerExit: " + eventData.enterEventCamera.gameObject);
        isControllerFocus = false;
        HideCanvas();
    }

    public void OnPointerHover(PointerEventData eventData) {
        //Log.d(LOG_TAG, "OnPointerHover: " + eventData.enterEventCamera.gameObject);
    }

    private void HideCanvas() {
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    private void ShowCanvas() {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
