using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
    public Button ScoreShow;
    private int ScoreCount, ScoreFile;
    public Text Txt1;
    public Image Hear1;
    public Image Hear2;
    public Image Hear3;
    private int Life;
    public Text DiaxRef;
    public int DiaxCount, DiaxCountFile;
    String File;

    private int DotIndex;
    
    
    // Use this for initialization
    void Start () {
        //Read();    

        Debug.Log(File);
        ScoreCount = ScoreShow.GetComponent<BlockCreate>().CubeCurrent;
        Life = ScoreShow.GetComponent<BlockCreate>().Life;
        //Debug.Log(File.text);
        //DotIndex = File.LastIndexOf(",");
        //DiaxCountFile = ((IntParseFast(File.Substring(0, DotIndex))));
        if (PlayerPrefs.HasKey("Diax"))
        {
            DiaxCountFile = PlayerPrefs.GetInt("Diax");
        }
        else
        {
            DiaxCountFile = 0;
        }

        //ScoreFile = ((IntParseFast(File.Substring(DotIndex+1))));
        if(PlayerPrefs.HasKey("HighScore"))
        {
            ScoreFile = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            ScoreFile = 0;
        }
        //ScoreFile = PlayerPrefs.GetInt("HighScore");
        Debug.Log(DiaxCountFile.ToString() + " " + ScoreFile.ToString());

        



    }
	
	// Update is called once per frame
	void Update () {
        ScoreCount = ScoreShow.GetComponent<BlockCreate>().CubeCurrent;
        if (ScoreShow.GetComponent<BlockCreate>().BStatus < 2)
        {
            Txt1.text = ScoreFile.ToString();
        }
        else
        {
            Txt1.text = ScoreCount.ToString();
        }
        Life = ScoreShow.GetComponent<BlockCreate>().Life;

        HeartRemain();
        DiaxRef.text = (DiaxCount +DiaxCountFile).ToString();


    }

    void HeartRemain()
    {
        switch (Life)
        {
            case 2:
                Hear1.GetComponent<CanvasGroup>().alpha = 0f;
                Hear1.GetComponent<CanvasGroup>().blocksRaycasts = false;
                break;
            case 1:
                Hear2.GetComponent<CanvasGroup>().alpha = 0f;
                Hear2.GetComponent<CanvasGroup>().blocksRaycasts = false;
                break;
            case 0:
                Hear3.GetComponent<CanvasGroup>().alpha = 0f;
                Hear3.GetComponent<CanvasGroup>().blocksRaycasts = false;
                //DiaxCountFile += DiaxCount;
                if (ScoreCount > ScoreFile)
                {
                    PlayerPrefs.SetInt("HighScore", ScoreCount);
                }


                PlayerPrefs.SetInt("Diax", int.Parse(DiaxRef.text));
                PlayerPrefs.Save();
                break;

        }
    }

    void Reload()
    {
        SceneManager.UnloadSceneAsync("Yea");
        SceneManager.LoadScene("Yea");
    }

    public static int IntParseFast(string value)
    {
        int result = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char letter = value[i];
            result = 10 * result + (letter - 48);
        }
        return result;
    }
}
