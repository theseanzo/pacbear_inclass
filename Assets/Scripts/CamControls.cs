using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{
    Transform target;
    Vector3 offset;
    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3((float)(GameManager.instance.grid.GetLength(1) - 1) / 2.0f, 5, (float)(GameManager.instance.grid.GetLength(0) + 1) / 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
