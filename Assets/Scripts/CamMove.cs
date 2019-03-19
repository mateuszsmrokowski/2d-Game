using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {
    public Transform target;
    public bool CamActive = true;

    Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = target.transform.position - this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (CamActive == true)
        {
            Vector3 Req = target.transform.position - offset;
            this.transform.position = Vector3.Lerp(this.transform.position, Req, 1.5f);
        } 
		
	}
}
