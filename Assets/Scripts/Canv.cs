using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canv : MonoBehaviour {
    public GameObject GO;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Round", 0.0f, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Round()
    {
        GO.GetComponent<Transform>().Rotate(0f, 50f*Time.deltaTime, 0f);

        //GO.GetComponent<Transform>().position = 
    }
}
