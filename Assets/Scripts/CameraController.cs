using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
	public float dragSpeed = 2;
	public float scrollSpeed = 1;

	Camera cam;
	Transform thisTransform;
	Vector3 currentPos;
     
    void Drag() // перенос камеры с зажатием ЛКМ
    {
    	if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) // проверка чтобы было
            currentPos = Input.mousePosition;
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()) // приятнее работать с GUI
        {
            Vector3 change = currentPos - Input.mousePosition;
            transform.Translate(change * dragSpeed);
            currentPos = Input.mousePosition;
        }
    }

    void Scroll() // изменение приближения камеры кручением колёсика мыши
    {
    	if (Input.GetAxis("Mouse ScrollWheel") > 0 && !EventSystem.current.IsPointerOverGameObject())
            if (cam.fieldOfView > 1)
                cam.fieldOfView -= scrollSpeed;

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && !EventSystem.current.IsPointerOverGameObject())
            if (cam.fieldOfView < 100)
                cam.fieldOfView += scrollSpeed;
    }

    void Start()
    {
        cam = Camera.main;
        thisTransform = transform;
    }

    void Update()
    {
        Drag();
        Scroll();        
    }
}