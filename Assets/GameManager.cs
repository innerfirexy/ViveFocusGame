using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using WaveVR_Log;
using wvr;

public class GameManager : MonoBehaviour {
    public GameObject wallPrefab;
    public float wallWidth = 10.0f;
    public float wallHeight = 20f;
    public float roomRadius = 200.0f;

    private void CreateWalls()
    {
        int numWalls1 = (int) Mathf.Ceil(2 * Mathf.PI * roomRadius / wallWidth);
        for (int i = 0; i < numWalls1; i++)
        {
            float angle = i * Mathf.PI * 2 / numWalls1;
            float x = Mathf.Cos(angle) * roomRadius;
            float z = Mathf.Sin(angle) * roomRadius;
            
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);

            Instantiate(wallPrefab, pos, rot);
        }
    }

	// Use this for initialization
	void Start () {
        //CreateWalls();
        Debug.Log("Start() is called.");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        Debug.Log("OnEnable is triggered ######################");
        CreateWalls(); //⚠️NOTE: it won't work if we put CreateWalls() in Start.
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable is triggered ***********************");
    }
}
