using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class BlockCreate : MonoBehaviour {
    private float Tim;
    public Button BGameStart;
    private int Left = 0, Right = 0;
    private double Way, LChance, RChance;
    private System.Random rnd, LightRand, FrozenRand, SpikesChance;
    public List<GameObject> Cubes = new List<GameObject>();
    public int CC = 0, BStatus = 0;
    public Camera Cam;
    private float Move = 0.01f;
    float speed = 1.0F;
    float startTime;
    float JLen;
    float FJour;
    float DistCov;
    public MeshRenderer DefMat;
    public DigitalRuby.LightningBolt.LightningBoltScript Scp;
    public Move MainSph;
    public int CubeCurrent = 0;
    public GameObject FirstPlatform;
    public int Life = 3;
    public Button Res;
    private double DiaxChance;
    public GameObject Diaxes;
    public GameObject FrozenRef;
    private RainCameraController FrozenEffect;
    private int LastSpike, LastDiax;
    public AudioClip CollectSound;
    public AudioClip FreezySound;
    private AudioSource AS;



    Material Mat;
    // Use this for initialization
    void Start() {
        Tim = Time.time;
        RChance = 0.5;
        LChance = 0.5;
        Cubes = new List<GameObject>();
        startTime = Time.time;
        //InvokeRepeating("LightDestroy", 4.0f, 1.0f);
        Mat = DefMat.GetComponent<MeshRenderer>().materials[0];
        MainSph = MainSph.GetComponent<Move>();
        InvokeRepeating("BlockDestroy", 0.0f, 0.1f);
        Scp.ManualMode = true;
        InvokeRepeating("CurrentCube", 0.0f, 0.01f);
        Screen.SetResolution(720, 1280, true);
        Res.GetComponent<CanvasGroup>().alpha = 0f;
        Res.interactable = false;
        InvokeRepeating("Frozen", 0f, 10f);
        FrozenEffect = FrozenRef.GetComponent<RainCameraController>();
        AS = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void FixedUpdate() {
        Tim = Time.time;


        BGameStart.onClick.AddListener(ButtClick);
        Res.onClick.AddListener(ResClick);
        if (BStatus == 1)
        {
            InvokeRepeating("CubeCreate", 0.0f, 0.1f);
            BStatus = 2;
            MainSph.GetComponent<Move>().Direct.GetComponent<CanvasGroup>().interactable = true;
            MainSph.GetComponent<Move>().JumpButt.GetComponent<CanvasGroup>().interactable = true;
        }

        if ((MainSph.transform.position.y < 0.55) && (Life > 0) && Res.GetComponent<CanvasGroup>().alpha == 0f)
        {
            Resurrection();
            MainSph.GetComponent<Move>().Direct.GetComponent<CanvasGroup>().interactable = false;
            MainSph.GetComponent<Move>().JumpButt.GetComponent<CanvasGroup>().interactable = false;
        }
        else if (Life == 0)
        {
            Dead();
        }

    }

    void CubeCreate() {
        if (CC <= CubeCurrent+50)
        {
            if (CC == 0)
            {
                Cubes.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
                Cubes[CC].transform.position = new Vector3(0, 0, 0);
                Cubes[CC].transform.localScale = new Vector3(1, 1, 1);
                Cubes[CC].transform.localScale = new Vector3(1, 1, 1);
                Cubes[CC].GetComponent<Renderer>().material = Mat;
                CC++;
            }

            Cubes.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
            Cubes[CC].GetComponent<Renderer>().material = Mat;

            rnd = new System.Random();
            Way = rnd.NextDouble();

            if ((Way >= 0.5))
            {
                Cubes[CC].transform.position = new Vector3((Cubes[CC - 1].transform.position.x + Cubes[CC - 1].transform.localScale.x), 0, Cubes[CC - 1].transform.position.z);
            }
            if ((Way < 0.5))
            {
                Cubes[CC].transform.position = new Vector3(Cubes[CC - 1].transform.position.x, 0, (Cubes[CC - 1].transform.position.z + Cubes[CC - 1].transform.localScale.z));
            }
            Cubes[CC].transform.localScale = new Vector3(1, 1, 1);

            rnd = new System.Random();
            DiaxChance = rnd.NextDouble();

            if ((DiaxChance > 0.88) && (CC > 5))
            {
                this.GetComponent<Collectable>().MakeNewColl(new Vector3(Cubes[CC - 2].transform.position.x, 0.7f, Cubes[CC - 2].transform.position.z), new Quaternion(0f, 0f, 0f, 0f));
                LastDiax = CC - 2;
            }
            else if ((CC > 10) && (CubeCurrent >= 0) && (BStatus > 0))
            {

                //SpikeCreate();
            }

            CC++;
        }
    }


    void ButtClick()
    {
        if (BStatus == 0)
        {
            BStatus = 1;
            BGameStart.GetComponent<CanvasGroup>().alpha = 0f;
            BGameStart.interactable = false;

        }
    }

    void Dead()
    {
        
        Invoke("Reload", 0f);
    }

    void Reload()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Resurrection()
    {
        Life--;
        Cam.GetComponent<CamMove>().CamActive = false;
        if (Life > 0)
        {
            Res.GetComponent<CanvasGroup>().alpha = 1f;
            Res.interactable = true;
        }



    }

    void ResClick()
    {
        Res.GetComponent<CanvasGroup>().alpha = 0f;
        Res.interactable = false;
        MainSph.GetComponent<Rigidbody>().velocity = Vector3.zero;
        MainSph.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        MainSph.transform.position = new Vector3(Cubes[CubeCurrent].transform.position.x, 0.6f, Cubes[CubeCurrent].transform.position.z);
        Cam.GetComponent<CamMove>().CamActive = true;
        MainSph.GetComponent<Move>().Direct.GetComponent<CanvasGroup>().interactable = true;
        MainSph.GetComponent<Move>().JumpButt.GetComponent<CanvasGroup>().interactable = true;
    }

    void BlockDestroy()
    {
        if (CubeCurrent == 1)
        {
            FirstPlatform.AddComponent<Rigidbody>();
        }
        if (CubeCurrent > 2)
        {
            Cubes[CubeCurrent - 3].AddComponent<Rigidbody>();

        }
        
    }

    void CurrentCube()
    {
        if (Cubes.Count > 1)
        {
            for (int i = 0 + CubeCurrent; i < Cubes.Count; i++)
            {
                if (Cubes[i].transform.position == MainSph.CubesVec)
                {
                    CubeCurrent = i;
                    break;
                }
            }
        }
    }
 
    void LightDestroy()
    {
        double Result;
        
        LightRand = new System.Random();
        Result = LightRand.NextDouble();
        if ((Result > 0.8) && (Cubes.Count > 10))
        {
            if (((Cubes[CubeCurrent + 1].transform.position.x == (Cubes[CubeCurrent + 2].transform.position.x)) && ((Cubes[CubeCurrent + 2].transform.position.x) == (Cubes[CubeCurrent + 3].transform.position.x)))
            || (((Cubes[CubeCurrent + 1].transform.position.z == (Cubes[CubeCurrent + 2].transform.position.z)) && ((Cubes[CubeCurrent + 2].transform.position.z) == (Cubes[CubeCurrent + 3].transform.position.z)))))
            {
                AS.PlayOneShot(CollectSound);
                Scp.StartPosition = new Vector3(Cam.transform.position.x + 5f, Cam.transform.position.y, Cam.transform.position.z + 15f);
                Scp.EndPosition = Cubes[CubeCurrent + 2].transform.position;
                Scp.Trigger();
                Cubes[CubeCurrent + 2].transform.localScale = new Vector3(0f, 0f, 0f);
            }
            
        }



    }

    void Frozen()
    {
        double Result;

        FrozenRand = new System.Random();
        Result = FrozenRand.NextDouble();
        if (Result > 0.1)
        {
            FrozenTimer();
        }

    }

    void FrozenTimer()
    {
        float TimeStart = Time.time;
        AS.PlayOneShot(FreezySound);
        FrozenEffect.Play();
        Invoke("FrozenStop", 3f);
    }
    void FrozenStop()
    {
        FrozenEffect.Stop();
    }

    void SpikeCreate()
    {


        if ((( Cubes[CC - 1].transform.position.x == (Cubes[CC - 2].transform.position.x)) &&  ((Cubes[CC - 2].transform.position.x) == (Cubes[CC - 3].transform.position.x))) 
        || (((Cubes[CC - 1].transform.position.z == (Cubes[CC - 2].transform.position.z)) && ((Cubes[CC - 2].transform.position.z) == (Cubes[CC - 3].transform.position.z)))))
        {

            double Result;
            SpikesChance = new System.Random();
            Result = FrozenRand.NextDouble();
            if (( Result > 0.5 ) && ( LastSpike+1 != CC-2 ) && (LastSpike + 2 != CC - 2) && LastSpike != LastDiax)
            {
                GameObject Spike = Instantiate(Resources.Load("Spikes")) as GameObject;
                Spike.transform.position = new Vector3(Cubes[CC -2 ].transform.position.x, 0.4f, Cubes[CC - 2].transform.position.z);
                LastSpike = CC - 2;
            }
            
        }



    }

}

