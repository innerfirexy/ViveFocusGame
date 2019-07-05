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

	// Use this for initialization
	void Start () {
        BeginGame();
	}

    private void BeginGame() {
        if (roomType == 1) {
            room1Instance = Instantiate(room1Prefab);
        }
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

    void Update()
    {
        
    }
}
