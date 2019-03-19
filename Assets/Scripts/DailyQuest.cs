using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DailyQuest : MonoBehaviour {
    public Button Daily;
    private bool show;
    private string[,] QuestArray = new string[7, 3]; // 0 - Opis, 1 - Nagroda, 2- Ile Zdobyc
    private int CurrentDay, Prize;
    public Text Desc, HowMuch;
    public Image Fill;
    public Button StartStatus;
    private int StartStatusInt;
    public GameObject Diax;
    private int DiaxStartCount;
    private int PrefInt;



	// Use this for initialization
	void Start () {

        Daily.onClick.AddListener(QuestShow);
        show = false;

        QuestArray[0, 0] = "Avoid Spike's 10 times";
        QuestArray[0, 1] = "100";
        QuestArray[0, 2] = "10";

        QuestArray[1, 0] = "Avoid Spike's 10 times";
        QuestArray[1, 1] = "100";
        QuestArray[1, 2] = "100";

        //QuestArray[2, 0] = "Avoid Spike's 10 times";
        QuestArray[2, 0] = "Collect 100 Diamond's ";
        QuestArray[2, 1] = "100";
        QuestArray[2, 2] = "100";

        QuestArray[3, 0] = "Collect 100 Diamond's ";
        QuestArray[3, 1] = "100";
        QuestArray[3, 2] = "100";

        QuestArray[4, 0] = "Die 3 times after reach 100 point's in another games";
        QuestArray[4, 1] = "100";
        QuestArray[4, 2] = "3";

        QuestArray[5, 0] = "Avoid Spike's 10 times";
        QuestArray[5, 1] = "100";
        QuestArray[5, 2] = "10";

        QuestArray[6, 0] = "Avoid Spike's 10 times";
        QuestArray[6, 1] = "100";
        QuestArray[6, 2] = "10";

        //Debug.Log(System.DateTime.Now.);
        CurrentDay = ((int)(DateTime.Now.DayOfWeek))-1;

        Desc.text = QuestArray[CurrentDay, 0].ToString();
        Prize = int.Parse(QuestArray[CurrentDay, 1]);

        PrefInt = PlayerPrefs.GetInt("QuestProgress");
        if (PlayerPrefs.HasKey("QuestProgress"))
        {
            //Fill.fillAmount = (PlayerPrefs.GetInt("QuestProgress") / int.Parse(QuestArray[CurrentDay, 2]));
            HowMuch.text = PrefInt.ToString() + "/" + QuestArray[CurrentDay, 2].ToString();
            Debug.Log("Text" + PrefInt);
        }
        else
        {
            //Fill.fillAmount = (0.0f / int.Parse(QuestArray[CurrentDay, 2]));
            HowMuch.text = "0/" + QuestArray[CurrentDay, 2].ToString();
            Debug.Log("TextNope");
        }

        if (PlayerPrefs.HasKey("QuestProgress"))
        {
            Debug.Log("Fill " + PrefInt);
            Fill.fillAmount = PrefInt / float.Parse(QuestArray[CurrentDay, 2]);
        }
        else
        {
            Fill.fillAmount = (0.0f / int.Parse(QuestArray[CurrentDay, 2]));
            Debug.Log("FillNope");
        }
        //Fill.fillAmount = (0.0f / int.Parse(QuestArray[CurrentDay, 2]));

        //Debug.Log(Application.persistentDataPath);
    }
	
	// Update is called once per frame
	void Update () {

        StartStatusInt = StartStatus.GetComponent<BlockCreate>().BStatus;
        if (StartStatusInt == 0)
        {
            Daily.GetComponent<CanvasGroup>().alpha = 1f;
            Daily.interactable = true;
        }
        else
        {
            //Daily.GetComponent<CanvasGroup>().alpha = 0f;
            //Daily.interactable = false;
            //show = false;
        }

        HowMuch.text = ((Diax.GetComponent<Score>().DiaxCount - DiaxStartCount + PrefInt).ToString() ) + "/" + QuestArray[CurrentDay, 2];
        Fill.fillAmount = ((Diax.GetComponent<Score>().DiaxCount - DiaxStartCount + PrefInt) / float.Parse(QuestArray[CurrentDay, 2]));

        PlayerPrefs.SetInt("QuestProgress", (Diax.GetComponent<Score>().DiaxCount - DiaxStartCount + PrefInt));
        PlayerPrefs.Save();

    }

    void QuestShow()
    {
        if (show == false)
        {
            Daily.GetComponent<RectTransform>().localPosition = new Vector3(-180f, Daily.GetComponent<RectTransform>().localPosition.y, Daily.GetComponent<RectTransform>().localPosition.z);
            show = true;
        }
        else
        {
            Daily.GetComponent<RectTransform>().localPosition = new Vector3(300f, Daily.GetComponent<RectTransform>().localPosition.y, Daily.GetComponent<RectTransform>().localPosition.z);
            show = false;
        }


    }

    void DiaxRead()
    {
        DiaxStartCount = Diax.GetComponent<Score>().DiaxCountFile;
    }
}
