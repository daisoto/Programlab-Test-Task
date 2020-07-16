using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class StandManager : MonoBehaviour
{
	public GameObject carSelect, firstCar;

	List<GameObject> cars;
	List<string> carNames;
	Dropdown dropSelectCar;
	Transform thisTransform;

	public void OpenNewCar() // открытие нового автомобиля из файла
	{
	    string path = EditorUtility.OpenFilePanel("Open new car prefab", "", "prefab"); 
	    string[] splitted = path.Split(new char[] { '/' });
	    bool found = false;
	    string final_path = "Assets"; // "Assets/Path/test.prefab"
	    foreach (string str in splitted)
	    {
	    	if (str == "Assets")
	    	{
	    		found = true;
	    		continue;
	    	}
	    	if (found)
	    		final_path += '/' + str;
	    } // нужен строго путь от папки с текущим проектом

	    Object carObj = AssetDatabase.LoadAssetAtPath(final_path, typeof(GameObject));
        GameObject car = Instantiate(carObj, thisTransform) as GameObject;
        car.transform.eulerAngles = new Vector3(0, 90, 0); // ставим поворот по умолчанию
        cars.Add(car);
        car.SetActive(false);
        car.name = car.name.Replace("(Clone)", ""); // удаляем лишнее из названия
        carNames.Add(car.name);
        dropSelectCar.ClearOptions();
        dropSelectCar.AddOptions(carNames);
	}

	void HideCars(GameObject currentCar) // скрываем все автомобили, кроме того что отображается
    {
        foreach (GameObject car in cars) 
        {
        	if (GameObject.ReferenceEquals(car, currentCar))
        	{
        		car.SetActive(true);
        		continue;
        	}
            car.SetActive(false);
        }
    }

	public void ChangeCar() // вызывается при выборе автомобиля в GUI
	{
		cars[dropSelectCar.value].transform.SetSiblingIndex(0);
		HideCars(cars[dropSelectCar.value]);
	}

    void Start()
    {
        dropSelectCar = carSelect.GetComponent<Dropdown>();
        cars = new List<GameObject>() {firstCar};
        carNames = new List<string>() {"car 1203 black"};
        thisTransform = transform;
    }

    void Update()
    {
        
    }
}
