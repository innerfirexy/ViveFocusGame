﻿using System.Collections;
using System.Collections.Generic;
using System;
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
    WaveVR_Controller.EDeviceType dirControllerType = WaveVR_Controller.EDeviceType.Head;
    public bool isTouchpadDown;
    private GameObject waveVRObj;
    private GameObject headObj;

    CharacterController characterController;
    public float speed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start () {
        originalPos = transform.position;
        //Log.d(LOG_TAG, "Start called.");
        isTouchpadDown = false;

        waveVRObj = transform.GetChild(0).gameObject;
        headObj = waveVRObj.transform.GetChild(0).gameObject; // Can use this to get the height of camera
        characterController = GetComponent<CharacterController>();

        Log.d(LOG_TAG, "child count of Subject: " + transform.childCount.ToString());
        Log.d(LOG_TAG, "child count of WaveVR: " + waveVRObj.transform.childCount.ToString());
        Log.d(LOG_TAG, "isTouchpadDown: " + isTouchpadDown);
        //DebugInfo();
    }
	
	// Update is called once per frame
	void Update () {
        if (WaveVR_Controller.Input(curFocusControllerType).GetPress(WVR_InputId.WVR_InputId_Alias1_Touchpad)) {
            isTouchpadDown = true;
        } else {
            isTouchpadDown = false;
        }

        if (isTouchpadDown)
        {
            moveForward();
            //DebugInfo();
        }
	}

    void DebugInfo()
    {
        Vector3 headFwd = headObj.transform.forward;
        Log.d(LOG_TAG, "head transform forward: " + headFwd.ToString());
    }

    void moveForward()
    {
        moveDirection = headObj.transform.forward;
        moveDirection.y = 0f;
        moveDirection *= speed;
        characterController.Move(moveDirection * Time.deltaTime);
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