using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    private RectTransform reticle;

    [SerializeField] float restingSize;
    [SerializeField] float maxSize;
    [SerializeField] float speed;

    float currentSize;

    private void Start()
    {
        reticle = GetComponent<RectTransform>();
    }


    void Update()
    {
        if (isMoving())
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        } else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }

        reticle.sizeDelta = new Vector2(currentSize, currentSize);
    }

    bool isMoving()
    {
        if (Input.GetAxis("Horizontal") != 0 ||
            Input.GetAxis("Vertical") != 0)
            return true;
        return false;
    }
}
