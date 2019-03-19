using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour {
    public Rigidbody rb;
    public Button Bot;
    public GameObject Text1;
    private float thrust = 40.0f;
    public ConstantForce CF;
    Vector3 Mov = new Vector3(1.0f, 0.0f, 0.0f);
    public bool Way = false;
    private bool Play = false;
    private int Life = 3;
    public int CubesOn;
    public Vector3 CubesVec;
    public GameObject ScoreRef;
    public GameObject CreateRef;
    private int InvokeBlock = 0;
    public int ControlTap = 0;
    private int TapStatus = 0;
    private float BotCd;
    public Button Direct, JumpButt;
    public AudioClip CollectSound;
    private AudioSource AS;
    public Transform SpherePos;
    public GameObject CubesRef;
    private List<GameObject> CubesList;
    private int CurrentObject;
    private int botway;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        //CubesRef = rb.GetComponent<BlockCreate>().Cubes;
        
        //Bot.interactable = false;
        //Bot.transform.localScale = (new Vector3(0, 0, 0));
        Direct.onClick.AddListener(TapStart);
        JumpButt.onClick.AddListener(Jump);
        AS = GetComponent<AudioSource>();
        Bot.onClick.AddListener(BotStart);
        //CubesRef = CubesRef.GetComponent<BlockCreate>().Cubes;
        //SpherePos = rb.transform;
    }

	void Update () {

        CubesList = CubesRef.GetComponent<BlockCreate>().Cubes;
        CurrentObject = CreateRef.GetComponent<BlockCreate>().CubeCurrent;
    }

    private void FixedUpdate()
    {
        if (CreateRef.GetComponent<BlockCreate>().BStatus == 0)
        {
            Direct.GetComponent<CanvasGroup>().alpha = 0f;
            Direct.GetComponent<CanvasGroup>().blocksRaycasts = false;
            JumpButt.GetComponent<CanvasGroup>().alpha = 0f;
            JumpButt.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            Direct.GetComponent<CanvasGroup>().alpha = 1f;
            Direct.GetComponent<CanvasGroup>().blocksRaycasts = true;
            JumpButt.GetComponent<CanvasGroup>().alpha = 1f;
            JumpButt.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
       
    }

    void TapStart()
    {

        if (( rb.transform.position.y >= 0.6f) && (rb.transform.position.y < 0.68f ))
        {

            if (Way == false)
            {

                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                CF.force = new Vector3(thrust, 0.0f, 0.0f);




            }
            if (Way == true)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                CF.force = new Vector3(0.0f, 0.0f, thrust);
            }

            Way = !Way;
        }
    }

    void Down()
    {
        rb.velocity = new Vector3(0f, 5f, 0f);
        Text1.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CubesVec = collision.gameObject.transform.position;

        if (( collision.gameObject.name == "Collectable") )
        {
            Destroy(collision.gameObject);
            ScoreRef.GetComponent<Score>().DiaxCount++;
            AS.PlayOneShot(CollectSound);

        }


    }


    void Jump()
    {
        if (Way == true)
        {
            rb.AddForce(new Vector3(0f, 800f, 0f));
            CF.force = new Vector3(0.0f, 0.0f, 0.0f);
            Invoke("ContinuedWay", 1f);
        }
        else
        {
            rb.AddForce(new Vector3(0f, 800f, 0f));
            CF.force = new Vector3(0.0f, 0.0f, 0.0f);
            Invoke("ContinuedWay", 1f);
        }
    }

    void ContinuedWay()
    {
        if (Way == true)
        {
            CF.force = new Vector3(thrust, 0.0f, 0.0f);
        }
        else
        {
            CF.force = new Vector3(0.0f, 0.0f, thrust);
        }
    }

    void BotStart()
    {
        InvokeRepeating("PlayBot", 0f, 0.01f);
    }

    void PlayBot()
    {
        botway = 0;
        if (CurrentObject > 0)

        {
            //Debug.Log((rb.transform.position.x + 1f).ToString() + " " +  (rb.transform.position.y - 0.5f).ToString() + " " + (rb.transform.position.z).ToString());
            //Debug.Log((rb.transform.position.x ).ToString() + " " + (rb.transform.position.y - 0.5f).ToString() + " " + (rb.transform.position.z+1f).ToString());
            if (Way == true)
            {
                if ((CubesList[CurrentObject + 1].GetComponent<Collider>().bounds.Contains(new Vector3(rb.transform.position.x + 0.95f, rb.transform.position.y - 0.5f, rb.transform.position.z)) == false )
                    && (CubesList[CurrentObject + 2].GetComponent<Collider>().bounds.Contains(new Vector3(rb.transform.position.x + 1.9f, rb.transform.position.y - 0.5f, rb.transform.position.z)) == false ))
                {
                    //Debug.Log("Brak, trza skrecic 1");
                    //TapStart();
                    botway = 1;
                    //prawo --> true
                }
            }

            if (Way == false)
            {
                if (( CubesList[CurrentObject + 1].GetComponent<Collider>().bounds.Contains(new Vector3(rb.transform.position.x, rb.transform.position.y - 0.5f, rb.transform.position.z + 0.95f)) == false)
                    && ( CubesList[CurrentObject + 2].GetComponent<Collider>().bounds.Contains(new Vector3(rb.transform.position.x, rb.transform.position.y - 0.5f, rb.transform.position.z + 1.9f)) == false))
                {
                    //Debug.Log("Brak, trza skrecic 2");
                    //TapStart();
                    botway = 2;
                    //lewo --> true
                }
            }

            if ((botway == 1) || (botway == 2))
            {
                if (((Time.time - BotCd) > 0.15))
                {
                    BotCd = Time.time;
                    TapStart();
                }
            }


        }

    }






}
