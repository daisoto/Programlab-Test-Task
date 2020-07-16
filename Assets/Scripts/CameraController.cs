using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float dragSpeed = 2;
	public float scrollSpeed = 2;

	Camera cam;
	Transform thisTransform;
     
    void Drag()
    {
        if (Input.GetMouseButtonDown(0))
        {
        	print(thisTransform.position);
            //Vector3 dragOrigin = Input.mousePosition; 
            //Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            //Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);
            //thisTransform.Translate(move, Space.World);
            thisTransform.Translate(Vector3.right * -Input.GetAxis("Mouse X") * .2f);
            thisTransform.Translate(Vector3.up * -Input.GetAxis("Mouse Y") * .2f); 
        }
    }

    void Scroll()
    {
    	if (Input.GetAxis("Mouse ScrollWheel") > 0)
            if (cam.fieldOfView > 1)
                cam.fieldOfView -= scrollSpeed;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            if (cam.fieldOfView < 100)
                cam.fieldOfView += scrollSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        thisTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Drag();
        Scroll();        
    }

}