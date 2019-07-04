using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    public float y = 9f;

    public void Init(float x, float z, float height, float width)
    {
        transform.position = new Vector3(x, y, z);
        transform.localScale = new Vector3(1, height, width);
    }
}
