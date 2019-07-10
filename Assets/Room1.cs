using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1 : MonoBehaviour {
    public GameObject wallPrefab;
    public float wallWidth = 10.0f;
    public float wallHeight = 30f;
    public float wallYOffset = -3f;
    public float roomRadius = 150.0f;

    private void CreateFanWalls()
    {
        int numWalls = (int)Mathf.Ceil(2 * Mathf.PI * roomRadius / wallWidth);
        for (int i = 0; i < numWalls; i++)
        {
            float angle = i * Mathf.PI * 2 / numWalls;
            float x = Mathf.Cos(angle) * roomRadius;
            float z = Mathf.Sin(angle) * roomRadius;
            float y = wallYOffset + wallHeight / 2;

            Vector3 pos = transform.position + new Vector3(x, y, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);

            Instantiate(wallPrefab, pos, rot);
        }
    }

    private void CreateCrossWalls()
    {
        int length = (int) Mathf.Ceil(2 * roomRadius / wallWidth) / 2;
        for (int i = -length; i < length; i++){
            float angleDegrees = 0f;
            float x = 0f;
            float z = (i + 0.5f) * wallWidth;
            float y = wallYOffset + wallHeight / 2;

            Vector3 pos = transform.position + new Vector3(x, y, z);
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            Instantiate(wallPrefab, pos, rot);
        }

        for (int i = -length; i < length; i++) {
            float angleDegrees = 90f;
            float x = (i + 0.5f) * wallWidth;
            float z = 0f;
            float y = wallYOffset + wallHeight / 2;

            Vector3 pos = transform.position + new Vector3(x, y, z);
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            Instantiate(wallPrefab, pos, rot);
        }
    }

    // Use this for initialization
    void Start () {
        CreateFanWalls();
        CreateCrossWalls();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
