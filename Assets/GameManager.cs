using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using WaveVR_Log;
using wvr;

public class GameManager : MonoBehaviour {
    public int roomType;
    private Room1 room1Instance;
    public Room1 room1Prefab;
    private Room2 room2Instance;

    public GameObject botPrefab;
    private List<Vector3> botPositions;
    public int numBotTotal = 2;
    public int numBotSaved = 0;
    private List<int> savedBotIDs;

	// Use this for initialization
	void Start () {
        BeginGame();
	}

    private void Update() {
        if (numBotSaved == numBotTotal) {
            EndGame();
        }
    }

    private void BeginGame() {
        if (roomType == 1) {
            room1Instance = Instantiate(room1Prefab);
        }

        botPositions = new List<Vector3> { };
        savedBotIDs = new List<int>();

        //⚠️ Using Instantiate is problematic
        //SpawnBot();
    }

    private void EndGame() {
        StopAllCoroutines();
        if (room1Instance != null) {
            Destroy(room1Instance);
        }
        if (room2Instance != null) {
            Destroy(room1Instance);
        }
        Application.Quit();
    }

    private void BC_BotSaved(int botID) {
        numBotSaved += 1;
        savedBotIDs.Add(botID);
    }

    private void BC_Timesup() {
        EndGame();
    }

    /**
    private void SpawnBot()
    {
        Vector3 pos = new Vector3(20f, 0f, 40f);
        float angleDegrees = 45f;
        Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
        GameObject bot1 = Instantiate(botPrefab, pos, rot);

        pos = new Vector3(50f, 0f, 60f);
        angleDegrees = 90f;
        rot = Quaternion.Euler(0, angleDegrees, 0);
        GameObject bot2 = Instantiate(botPrefab, pos, rot);

        pos = new Vector3(20f, 0f, 10f);
        angleDegrees = 120f;
        rot = Quaternion.Euler(0, angleDegrees, 0);
        GameObject bot3 = Instantiate(botPrefab, pos, rot);
    }
    */
}
