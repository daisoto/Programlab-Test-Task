using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
	public GameObject detailSelect;

	GameObject[] elements;
	Transform thisTransform;
	Dropdown dropSelect;

    void SetElements()
    {
        elements = new GameObject[thisTransform.childCount];
        int i = 0;

        foreach (Transform child in thisTransform)
        {
            elements[i] = child.gameObject;
            i += 1;
        }
    }

    void HideElements(GameObject currentElement) // скрываем все элементы, кроме того что отображается
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

    void AssembleElements()
    {
    	foreach (GameObject element in elements) 
        {
            element.SetActive(true);
        }
    }

    void onValueChanged()
    {
    	if (dropSelect.value == 0)
    	{
    		AssembleElements();
    	}
    	else 
    	{
    		HideElements(elements[dropSelect.value]);
    	}
    }

    void Start()
    {
    	dropSelect = detailSelect.GetComponent<Dropdown>();
        thisTransform = transform;
        SetElements();
    }

    void Update()
    {
        
    }
}
