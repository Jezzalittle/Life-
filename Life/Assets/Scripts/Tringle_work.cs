using UnityEngine;
using System.Collections;

public class Tringle_work : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    public GameObject[] spices;
	// Update is called once per frame
	void Update () {


        if (spices == null)
            spices = GameObject.FindGameObjectsWithTag("food");

        foreach (GameObject temp in spices)
        {
            print(temp.transform);
        }


    }
}
