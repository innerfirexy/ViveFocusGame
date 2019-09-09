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

	// Use this for initialization
	void Start () {
        BeginGame();
	}

    private void BeginGame() {
        if (roomType == 1) {
            room1Instance = Instantiate(room1Prefab);
        }

        // Define bot positions
        botPositions = new List<Vector3> {
            new Vector3(20f, 0f, 40f),
        };

        // Spawn
        SpawnBot();
    }

    private void EndGame() {
        StopAllCoroutines();
        if (room1Instance != null) {
            Destroy(room1Instance);
        }
        if (room2Instance != null) {
            Destroy(room1Instance);
        }
    }

    private void SpawnBot()
    {
        Vector3 pos = new Vector3(20f, 0f, 40f);
        float angleDegrees = 45f;
        Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
        Instantiate(botPrefab, pos, rot);

        pos = new Vector3(50f, 0f, 60f);
        angleDegrees = 90f;
        rot = Quaternion.Euler(0, angleDegrees, 0);
        Instantiate(botPrefab, pos, rot);

        pos = new Vector3(20f, 0f, 10f);
        angleDegrees = 120f;
        rot = Quaternion.Euler(0, angleDegrees, 0);
        Instantiate(botPrefab, pos, rot);
    }

    void Update()
    {
        
    }
}
