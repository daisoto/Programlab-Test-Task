using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CarController : MonoBehaviour
{
	public GameObject detailSelect, inputSpeed, description;

    List<GameObject> elements;
	Transform carTransform, currentTransform;
	Dropdown dropSelectDetail;
    Text txtSpeed, txtDesc;
    float speed = 10;
    Vector3 currentAngle;
    bool isRotating = false;

    void SetElements() // создаем список деталей автомобиля 
    {
        elements = new List<GameObject>();
        List<string> options = new List<string>() {"Assembled"}; // вариант по умолчанию
        foreach (Transform child in carTransform)
        {
            if (child.gameObject.name == "Collider")
                continue;
            elements.Add(child.gameObject);
            options.Add(child.gameObject.name);
        }

        dropSelectDetail.ClearOptions();
        dropSelectDetail.AddOptions(options);
    }

    void HideElements(GameObject currentElement) // скрываем все детали, кроме той что отображается
    {
        foreach (GameObject element in elements) 
        {
        	if (GameObject.ReferenceEquals(element, currentElement))
        	{
        		element.SetActive(true);
        		continue;
        	}
            element.SetActive(false);
        }
    }

    void AssembleElements() // отображаем все детали автомобиля
    {
    	foreach (GameObject element in elements) 
            element.SetActive(true);
    }

    public void SetElement() // вызывается при выборе детали в GUI
    {
        ResetRotation();
    	if (dropSelectDetail.value == 0) // "собираем" автомобиль
    	{
    		AssembleElements();
            currentTransform = carTransform;
            description.SetActive(false);
    	}
    	else 
    	{
    		HideElements(elements[dropSelectDetail.value - 1]); // оставляем активным только текущую деталь
            currentTransform = elements[dropSelectDetail.value - 1].transform;
            SetDescription();
    	}
    }

    public void SetRotation() // устанавливаем флаг вращения
    {
        isRotating = !isRotating;
    }

    public void ResetRotation() // возвращаем в положение по умолчанию
    {
        isRotating = false;
        currentTransform.eulerAngles = new Vector3(0, 90, 0);
    }

    void Rotate() // поворот 
    {
        currentTransform.Rotate(0, speed * Time.deltaTime, 0);
    }

    void SetSpeed()
    {
        try
        {
            speed = (float)Convert.ToDouble(txtSpeed.text);
        }
        catch (FormatException)
        {
            return;
            //print("Wrong input format");
        }
    }

    void SetDescription() // пример появления описания без хардкода
    {
        GameObject curObj = currentTransform.gameObject;
        txtDesc.text = "This is " + curObj.name;
        description.SetActive(true);
    }

    public void Start()
    {
    	dropSelectDetail = detailSelect.GetComponent<Dropdown>();
        carTransform = transform.GetChild(0);
        currentTransform = carTransform;

        txtSpeed = inputSpeed.transform.GetChild(0).gameObject.GetComponent<Text>(); 
        if (!txtSpeed) // потому что после первого запуска сцены создает speedinputcaret который встает на 0 место
            txtSpeed = inputSpeed.transform.GetChild(1).gameObject.GetComponent<Text>();

        txtDesc = description.transform.GetChild(0).gameObject.GetComponent<Text>();
        print(txtSpeed);
        description.SetActive(false);
        SetElements();
        AssembleElements();
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
