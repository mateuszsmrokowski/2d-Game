using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureChange : MonoBehaviour {

    public List<Material> MaterialList = new List<Material>();
    public Button Right;
    public Button Left;
    public GameObject StartRef;
    private int StartStatus;
    public int i = 0;

    // Use this for initialization
    void Start () {
        MaterialList.Add(this.GetComponent<MeshRenderer>().materials[0]);
        Right.onClick.AddListener(RightButt);
        Left.onClick.AddListener(LeftButt);
        StartStatus = StartRef.GetComponent<BlockCreate>().BStatus;
    }
	
	// Update is called once per frame
	void Update () {
        StartStatus = StartRef.GetComponent<BlockCreate>().BStatus;
        if (StartStatus == 0)
        {
            Right.GetComponent<CanvasGroup>().alpha = 1f;
            Left.GetComponent<CanvasGroup>().alpha = 1f;
            Right.GetComponent<CanvasGroup>().interactable = true;
            Left.GetComponent<CanvasGroup>().interactable = true;
        }
        else
        {
            Right.GetComponent<CanvasGroup>().alpha = 0f;
            Left.GetComponent<CanvasGroup>().alpha = 0f;
            Right.GetComponent<CanvasGroup>().interactable = false;
            Left.GetComponent<CanvasGroup>().interactable = false;
        }
        this.GetComponent<Renderer>().material = MaterialList[i];

    }

    void RightButt()
    {
        if ((i+1) < MaterialList.Count)
        {
            i++;
        }
        else
        {
            i=0;
        }
    }

    void LeftButt()
    {
        if ((i - 1) == -1)
        {
            i = MaterialList.Count - 1;
        }
        else
        {
            i--;
        }
    }
}
