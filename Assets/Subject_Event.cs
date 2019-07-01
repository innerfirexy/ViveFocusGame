using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using WaveVR_Log;
using wvr;

public class Subject_Event : MonoBehaviour,
    IPointerUpHandler,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler,
    IPointerHoverHandler
{
    private const string LOG_TAG = "WaveVR_Subject";
    Vector3 originalPos;
    // 默认输入设备为主手柄 🎮
    WaveVR_Controller.EDeviceType curFocusControllerType = WaveVR_Controller.EDeviceType.Dominant;

    // Use this for initialization
    void Start () {
        originalPos = transform.position;
        //Log.d(LOG_TAG, "Start called.");
    }
	
	// Update is called once per frame
	void Update () {
            if (WaveVR_Controller.Input(curFocusControllerType).GetPressUp(WVR_InputId.WVR_InputId_Alias1_Touchpad) ||
            WaveVR_Controller.Input(curFocusControllerType).GetPressDown(WVR_InputId.WVR_InputId_Alias1_Digital_Trigger) ||
            WaveVR_Controller.Input(curFocusControllerType).GetPressDown(WVR_InputId.WVR_InputId_Alias1_Trigger))
            {
                moveForward();
            }
	}

    void moveForward()
    {
        Vector3 oldPos = transform.position;
        Vector3 newPos = new Vector3(oldPos.x + 1, oldPos.y, oldPos.z);
        transform.position = newPos;
        Log.d(LOG_TAG, "moveFroward called.");
    }

    

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("OnPointerUp");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
    }

    public void OnPointerHover(PointerEventData eventData)
    {
        // Debug.Log("OnPointerHover: "+eventData.enterEventCamera.gameObject);
    }
}
