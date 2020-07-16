using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ElementController : MonoBehaviour
{
	public GameObject input;

	private Text txt;
    private float speed = 10;
    private Transform thisTransform;
    private Vector3 currentAngle;
    private bool isRotating = false;

    public void SetRotation()
    {
    	isRotating = !isRotating;
    }

    public void ResetRotation()
    {
    	isRotating = false;
    	transform.eulerAngles = Vector3.zero;
    }

    void Rotate()
    {
    	transform.Rotate(0, speed * Time.deltaTime, 0);
    }

    void SetSpeed()
    {
        try
        {
            speed = (float)Convert.ToDouble(txt.text);
        }
        catch (FormatException)
        {
            //print("Wrong input format");
            return;
        }
    }

    void Start()
    {
        thisTransform = transform;
        txt = input.transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (isRotating)
        {
        	SetSpeed();
            Rotate();
        }
    }
}