using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using wvr;


[RequireComponent(typeof(CanvasGroup))]
public class HeadCanvas : MonoBehaviour {
    private GameObject FPS;
    private Text debugText;
    private GameObject cdTextGO;
    private Text cdText; // countdown Text
    private int timeLeft;

    private GameObject minimapCanvas;
    private GameObject minimapCameraObj;
    private Camera minimapCamera;

    WaveVR_Controller.EDeviceType mainControllerType = WaveVR_Controller.EDeviceType.Dominant;
    private CanvasGroup canvasGroup;
    private string logMsg;
    private Queue logMsgQueue = new Queue();

    private GameObject miniPlayerGO;
    private RectTransform miniPlayerRect;
    private float miniX0; // horizontal direction in minimap
    private float miniY0; // vertical direction in minimap
    private float miniScale;
    private Vector2 miniSource;
    private float sceneX0; // maps to miniX0
    private float sceneZ0; // maps to miniY0
    private Vector2 sceneSource;

    private GameObject gmGO;
    private GameManager gm;
    private GameObject subjectGO;
    private SubjectEvent subject;

    void Awake()
    {
        FPS = transform.GetChild(0).gameObject;
        debugText = FPS.GetComponent<Text>();
        debugText.text = "";
        cdTextGO = transform.GetChild(3).gameObject;
        cdText = cdTextGO.GetComponent<Text>();

        minimapCanvas = transform.GetChild(1).gameObject;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;

        miniPlayerGO = transform.Find("Player").gameObject;
        miniPlayerRect = miniPlayerGO.GetComponent<RectTransform>();
        miniX0 = -65f;
        miniY0 = -15f;
        miniScale = 150f/150f;
        miniSource = new Vector2(miniX0, miniY0);
        sceneX0 = 5; 
        sceneZ0 = 5;
        sceneSource = new Vector2(sceneX0, sceneZ0);

        gmGO = GameObject.Find("/Game Manager");
        gm = gmGO.GetComponent<GameManager>();
        subjectGO = GameObject.Find("/Subject");
        subject = subjectGO.GetComponent<SubjectEvent>();

        // Start timer
        timeLeft = gm.timeLimit;
        StartCoroutine("DecrementTime");
        Time.timeScale = 1; // Making sure that the timeScale is right
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
        //debugText.text = string.Format("Saved bot: {0}", gm.numBotSaved);
        debugText.text = string.Format("my pos: {0}", subject.GetPosition2D().ToString());
        //bot0 distance
        //float d0 = Vector2.Distance(subject.GetPosition2D(), new Vector2(20, 20));
        //float d1 = Vector2.Distance(subject.GetPosition2D(), new Vector2(16, 30));
        //debugText.text = string.Format("d0: {0}, d1: {1}", d0, d1);

        //Show minimap or not
        if (WaveVR_Controller.Input(mainControllerType).GetPress(WVR_InputId.WVR_InputId_Alias1_Menu)) {
            ShowCanvas();
        } else {
            HideCanvas();
        }

        // Update countdown time left
        cdText.text = "剩余时间：" + timeLeft;
        if (timeLeft <= 0) {
            StopCoroutine("DecrementTime");
            gmGO.BroadcastMessage("BC_Timesup", SendMessageOptions.DontRequireReceiver);
        }
    }

    IEnumerator DecrementTime() {
        while (true) {
            yield return new WaitForSeconds(1);
            timeLeft--;
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

    private void BC_PlayerMoved(Transform tr)
    {
        //textField.text = string.Format("player position: ({0}, {1})",
        //    miniPlayerRect.localPosition.x, miniPlayerRect.localPosition.y);

        Vector2 currScenePos = new Vector2(tr.position.x, tr.position.z);
        Vector2 vec = currScenePos - sceneSource;
        Vector2 miniNew = miniScale * vec + miniSource;
        float miniX1 = miniNew.x;
        float miniY1 = miniNew.y;

        float oldZ = miniPlayerRect.localPosition.z;
        miniPlayerRect.localPosition = new Vector3(miniX1, miniY1, oldZ);

        float degrees = Vector3.Angle(Vector3.forward, tr.forward);
        if (Vector3.Cross(Vector3.forward, tr.forward).y > 0) {
            degrees = -degrees;
        }
        miniPlayerRect.localRotation = Quaternion.Euler(0f, 0f, degrees);
    }

    private void BC_BotSaved(int botID)
    {
        //textField.text = string.Format("Bot #{0} saved", botID);
    }
}
