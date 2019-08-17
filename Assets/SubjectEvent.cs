using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using WaveVR_Log;
using wvr;

public class SubjectEvent : MonoBehaviour,
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
    WaveVR_Controller.EDeviceType mainControllerType = WaveVR_Controller.EDeviceType.Dominant;
    
    private bool isTouchpadDown;
    private bool isTriggerDown;
    private GameObject waveVRObj;
    private GameObject headObj;
    private GameObject headCamera;

    CharacterController characterController;
    public float speed = 8.0f;
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start () {
        originalPos = transform.position;
        //Log.d(LOG_TAG, "Start called.");
        isTouchpadDown = false;
        isTriggerDown = false;

        waveVRObj = transform.GetChild(0).gameObject;
        headObj = waveVRObj.transform.GetChild(0).gameObject; // Can use this to get the height of camera
        characterController = GetComponent<CharacterController>();

        //Log.d(LOG_TAG, "child count of Subject: " + transform.childCount.ToString());
        //Log.d(LOG_TAG, "child count of WaveVR: " + waveVRObj.transform.childCount.ToString());
        //Log.d(LOG_TAG, "isTouchpadDown: " + isTouchpadDown);
        //DebugInfo();
    }
	
	// Update is called once per frame
	void Update () {
        if (WaveVR_Controller.Input(mainControllerType).GetPress(WVR_InputId.WVR_InputId_Alias1_Touchpad)) {
            isTouchpadDown = true;
        } else {
            isTouchpadDown = false;
        }

        if (WaveVR_Controller.Input(mainControllerType).GetPress(WVR_InputId.WVR_InputId_Alias1_Trigger)) {
            isTriggerDown = true;
        } else {
            isTriggerDown = false;
        }

        // Move
        if (isTouchpadDown && !isTriggerDown) {
            Move(true);
        }
        if (!isTouchpadDown && isTriggerDown) {
            Move(false);
        }
	}

    void DebugInfo()
    {
        Vector3 headFwd = headObj.transform.forward;
        Log.d(LOG_TAG, "head transform forward: " + headFwd.ToString());
    }

    void Move(bool isForward)
    {
        moveDirection = headObj.transform.forward;
        if (!isForward) {
            moveDirection.Set(-moveDirection.x, 0f, -moveDirection.z);
        } else {
            moveDirection.y = 0f;
        }
        moveDirection *= speed;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    public Vector3 GetForward()
    {
        return headObj.transform.forward;
    }

    public Vector3 GetPosition()
    {
        return headObj.transform.position;
    }

    public Transform GetHeadTransform()
    {
        return headObj.transform;
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
