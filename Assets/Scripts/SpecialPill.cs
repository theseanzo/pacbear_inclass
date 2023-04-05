using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPill : BaseUnit
{

    [SerializeField] private float rotateSpeed = 50;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);
    }
}
