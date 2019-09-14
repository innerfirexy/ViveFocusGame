using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WaveVR_Log;
using wvr;


[RequireComponent(typeof(CanvasGroup))]
public class Bot : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
    //IPointerHoverHandler
{
    public int botID;
    public float reachableDistance = 10f;
    public float posX;
    public float posZ;
    private Vector2 myPos;

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
        isReachable = false;
        myPos = new Vector2(posX, posZ);

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
        if (!isSaved) {
            if (isControllerFocus && isReachable) {
                if (WaveVR_Controller.Input(mainControllerType).GetPress(WVR_InputId.WVR_InputId_Alias1_Touchpad))
                {
                    progress.Update();
                    barImage.fillAmount = progress.GetProgressNorm();
                    if (barImage.fillAmount >= 0.999f)
                    {
                        isSaved = true;
                    }
                }
                if (WaveVR_Controller.Input(mainControllerType).GetPressUp(WVR_InputId.WVR_InputId_Alias1_Touchpad))
                {
                    progress.Reset();
                    barImage.fillAmount = progress.GetProgressNorm();
                }
            }
            //Use else to handle isReachable == false
        }
        else {
            BecomeSaved();
        }
    }

    private void BecomeSaved() {
        gameObject.SetActive(false);
        subjectGO.BroadcastMessage("BC_BotSaved", botID, SendMessageOptions.DontRequireReceiver);
        gameManagerGO.BroadcastMessage("BC_BotSaved", botID, SendMessageOptions.DontRequireReceiver);
        subject.actStat = SubjectEvent.ActionStatus.Walk;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //Log.d(LOG_TAG, "OnPointerEnter: " + eventData.enterEventCamera.gameObject);
        isControllerFocus = true;
        isReachable = checkSubjectReachable();
        if (isReachable) {
            subject.actStat = SubjectEvent.ActionStatus.Save;
            ShowCanvas();
        }
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

    /**
    public void OnPointerHover(PointerEventData eventData) {
        isControllerFocus = true;
        //isReachable = checkSubjectReachable();
        if (isReachable) {
            subject.actStat = SubjectEvent.ActionStatus.Save;
            //Make the canvas face towards the VR camera
            Transform headTrans = subject.GetHeadTransform();
            Vector3 relativePos = -(headTrans.position - canvasTrans.position);
            canvasTrans.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            //Log.d(LOG_TAG, "head position: " + headTrans.position.ToString());
        }
    }
    */

    private void HideCanvas() {
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    private void ShowCanvas() {
        Transform headTrans = subject.GetHeadTransform();
        Vector3 relativePos = -(headTrans.position - canvasTrans.position);
        canvasTrans.rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    private bool checkSubjectReachable()
    {
        Vector2 subjectPos = subject.GetPosition2D();
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
