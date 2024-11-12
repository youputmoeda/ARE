using UnityEngine;
using System;
using System.Collections;

public class BobbingEffect : MonoBehaviour
{
    float originalY;

    [SerializeField] private float floatStrength = 1; // You can change this in the Unity Editor to 
                                                      // change the range of y positions that are possible.
    [SerializeField] private bool rotateY = true;
    [SerializeField] private bool rotateX = false;
    [SerializeField] private bool rotateZ = false;
    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(Time.time) * floatStrength),
            transform.position.z);

        if (rotateX)
            transform.Rotate(1, 0, 0);

        if (rotateY)
            transform.Rotate(0, 1, 0);

        if (rotateZ)
            transform.Rotate(0, 0, 1);
    }
}