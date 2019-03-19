using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
    public GameObject MasterObject;
    private GameObject Child;
	// Use this for initialization
	void Start () {
		
	}


    public void MakeNewColl(Vector3 trans, Quaternion Qua)
    {
       
        Child = Instantiate(MasterObject, trans, Qua);
        Child.name = "Collectable";
    }
}
