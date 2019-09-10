﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WaveVR_Log;
using wvr;


[RequireComponent(typeof(CanvasGroup))]
public class Bot : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerHoverHandler
{
    public int botID;
    public float reachableDistance = 50f;
    private const string LOG_TAG = "WaveVR_Bot";
    private bool isControllerFocus;
    private bool isSaved;
    private bool isReachable;

    private GameObject subjectGO;
    private SubjectEvent subject;
    private GameObject gameManagerGO;

    WaveVR_Controller.EDeviceType mainControllerType = WaveVR_Controller.EDeviceType.Dominant;
    private CanvasGroup canvasGroup;
    private Transform canvasTrans;

    private Transform barTrans;
    private Image barImage;
    private Progress progress;

	void Start () {
        isControllerFocus = false;
        isSaved = false;
        isReachable = true;

        subjectGO = GameObject.Find("/Subject");
        subject = subjectGO.GetComponent<SubjectEvent>();
        gameManagerGO = GameObject.Find("/Game Manager");

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasTrans = transform.GetChild(0).GetChild(3);

        barTrans = transform.GetChild(0).GetChild(3).GetChild(1).GetChild(2);
        barImage = barTrans.gameObject.GetComponent<Image>();
        //⚠️ Not clear why transform.Find("bar") causes problem here.
        barImage.fillAmount = 0.05f;
        progress = new Progress();

        //Log.d(LOG_TAG, "barTrans: " + barTrans.ToString());
        //Log.d(LOG_TAG, "barImage: " + barImage.ToString());
        //Log.d(LOG_TAG, "canvasTrans: " + canvasTrans.ToString());
    }

    // Update is called once per frame
    void Update () {
        if (!isSaved)
        {
            if (isControllerFocus && isReachable)
            {
                if (WaveVR_Controller.Input(mainControllerType).GetPress(WVR_InputId.WVR_InputId_Alias1_Menu))
                {
                    progress.Update();
                    barImage.fillAmount = progress.GetProgressNorm();
                    if (barImage.fillAmount >= 0.999f)
                    {
                        isSaved = true;
                    }
                }
                if (WaveVR_Controller.Input(mainControllerType).GetPressUp(WVR_InputId.WVR_InputId_Alias1_Menu))
                {
                    progress.Reset();
                    barImage.fillAmount = progress.GetProgressNorm();
                }
            }
            //Use else to handle isReachable == false
        }
        else
        {
            BecomeSaved();
        }
    }

    private void BecomeSaved() {
        gameObject.SetActive(false);
        subjectGO.BroadcastMessage("BC_BotSaved", botID, SendMessageOptions.DontRequireReceiver);
        gameManagerGO.BroadcastMessage("BC_BotSaved", botID, SendMessageOptions.DontRequireReceiver);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //Log.d(LOG_TAG, "OnPointerEnter: " + eventData.enterEventCamera.gameObject);
        isControllerFocus = true;
        isReachable = checkReachable();
        if (isReachable) {
            subject.actStat = SubjectEvent.ActionStatus.Save;
        }
        ShowCanvas();
    }

    public void OnPointerExit(PointerEventData eventData) {
        //Log.d(LOG_TAG, "OnPointerExit: " + eventData.enterEventCamera.gameObject);
        isControllerFocus = false;
        subject.actStat = SubjectEvent.ActionStatus.Walk;
        if (!isSaved) {
            progress.Reset();
            barImage.fillAmount = progress.GetProgressNorm();
        }
        HideCanvas();
    }

    public void OnPointerHover(PointerEventData eventData) {
        //Make the canvas face towards the VR camera
        Transform headTrans = subject.GetHeadTransform();
        Vector3 relativePos = -(headTrans.position - canvasTrans.position);
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        canvasTrans.rotation = rotation;
        //Log.d(LOG_TAG, "head position: " + headTrans.position.ToString());
    }

    private void HideCanvas() {
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    private void ShowCanvas() {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    private bool checkReachable()
    {
        Vector2 subjectPos = subject.GetPosition2D();
        Vector2 myPos = new Vector2(transform.position.x, transform.position.z);
        if (Vector2.Distance(subjectPos, myPos) <= reachableDistance) {
            return true;
        } else {
            return false;
        }
    }
}


public class Progress {
    public const int MAX_PROGRESS = 100;

    private float currentProgress;
    private float increaseSpeed;

    public Progress() {
        currentProgress = 5f;
        increaseSpeed = 30f;
    }

    public void Update() {
        currentProgress += increaseSpeed * Time.deltaTime;
    }

    public void Reset() {
        currentProgress = 5f;
    }

    public float GetProgressNorm() {
        return currentProgress / MAX_PROGRESS;
    }
}
