using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1 : MonoBehaviour {
    public GameObject wallPrefab;
    public float wallWidth = 10.0f;
    public float wallHeight = 20f;
    public float roomRadius = 100.0f;

    private void CreateFanWalls()
    {
        int numWalls = (int)Mathf.Ceil(2 * Mathf.PI * roomRadius / wallWidth);
        for (int i = 0; i < numWalls; i++)
        {
            float angle = i * Mathf.PI * 2 / numWalls;
            float x = Mathf.Cos(angle) * roomRadius;
            float z = Mathf.Sin(angle) * roomRadius;

            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);

            Instantiate(wallPrefab, pos, rot);
        }
    }

    private void CreateCrossWalls()
    {
        int numWalls = (int)Mathf.Ceil(2 * roomRadius / wallWidth);
        for (int i = 0; i < numWalls; i++)
        {
        }
        for (int i = 0; i < numWalls; i++)
        {
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
