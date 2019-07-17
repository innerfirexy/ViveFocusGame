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
    IPointerExitHandler,
    IPointerHoverHandler
{
    private const string LOG_TAG = "WaveVR_Bot";
    private bool isControllerFocus;
    private bool isSaved;
    private bool isReachable;
    WaveVR_Controller.EDeviceType mainControllerType = WaveVR_Controller.EDeviceType.Dominant;
    private CanvasGroup canvasGroup;

    private Image barImage;
    private Progress progress;

	void Start () {
        isControllerFocus = false;
        isSaved = false;
        isReachable = true;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;

        barImage = GameObject.Find("bar").GetComponent<Image>();
        //⚠️ Not clear why transform.Find("bar") causes problem here.
        barImage.fillAmount = 0.05f;
        progress = new Progress();

        //Log.d(LOG_TAG, "Bot started");
        //Log.d(LOG_TAG, "barImage: " + barImage.ToString());
        //Log.d(LOG_TAG, "barImage fillAmount: " + barImage.fillAmount.ToString());
        //Debug.Log("Bot started");
        //Debug.Log("barImage: " + barImage.ToString());
        //Debug.Log("barImage fillAmount: " + barImage.fillAmount.ToString());
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
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //Log.d(LOG_TAG, "OnPointerEnter: " + eventData.enterEventCamera.gameObject);
        isControllerFocus = true;
        ShowCanvas();

        //Make the canvas face towards the VR camera
        GameObject target = eventData.enterEventCamera.gameObject;
        Log.d(LOG_TAG, "onPointerEventData target: " + target.transform.localPosition.ToString());
    }

    public void OnPointerExit(PointerEventData eventData) {
        //Log.d(LOG_TAG, "OnPointerExit: " + eventData.enterEventCamera.gameObject);
        isControllerFocus = false;
        HideCanvas();

        if (!isSaved) {
            progress.Reset();
            barImage.fillAmount = progress.GetProgressNorm();
        }
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