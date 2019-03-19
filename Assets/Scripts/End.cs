using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour {
    public Rigidbody rb;
    public GameObject GB;
    public Text Txt1;
    public int Life = 3;
    // Use this for initialization
    void Start () {
        GB.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if ((Physics.Raycast(rb.transform.position, Vector3.down * 2) == false) && Life > 0)
        {
            Life--;

            
        }
        else
        {
            GB.SetActive(true);
        }


	}

    
}
