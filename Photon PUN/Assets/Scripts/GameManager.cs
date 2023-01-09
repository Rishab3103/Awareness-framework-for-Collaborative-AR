using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR;
using UnityEngine.XR.ARSubsystems;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPun
{

   //GameObject playingArena;
    //public GameObject cube;
    public GameObject sphere;
    
    public Vector3 m_Min, m_Max;
    public Vector3 mol_pos;
    public DestroyOnTouch mole;
    public float countdown = 60.0f;
    bool tracking;
    public NetworkManager network;
    //public ImageTracker image;

    
    public TextMeshProUGUI score_Text;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI YourScoreIs;
    public GameObject playAgain;
    public ARRaycastManager RaycastManager;
    
    Collider m_collider;
    public int Scorecount = 0;
    float x_dim;
    float y_dim;
    bool isInRoom;
    Vector3 arena_pos = new Vector3();
    public ARPlaneManager arPlaneMananger;
    public PlaneSpawner plane;

    public static GameManager Instance;
    public ImageTracker image;
    public GameObject arena;
    public GameObject Hammer;
    private PhotonView PV;
    public bool isActiveP1;
    public bool isActiveP2;
    public ARAnchorManager m_AnchorManager;
    public Material materialP1;
    public Material materialP2;
    public GameObject cube1;
    public OwnershipTransfer owner;
    int m_NumberOfTrackedImages;
    List<ARRaycastHit> HitResult = new List<ARRaycastHit>();
    void Start()
    {

        //Debug.Log(HitResult.Count);
        
        m_collider = GameObject.FindGameObjectsWithTag("arena")[0].GetComponent<BoxCollider>();
        score_Text = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();

        timer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();

        gameOver = GameObject.Find("GameOver").GetComponent<TextMeshProUGUI>();

        YourScoreIs = GameObject.Find("YourScoreIs").GetComponent<TextMeshProUGUI>();
        
        gameOver.text = "";

       
    }

    // Update is called once per frame
    void Update()
    {

        ScoreCard();
        Gameplay();

        
        
      

    }

    public void Gameplay()
    {
        
        if (countdown > 0 && isActiveP1 && isActiveP2)
        {
            
            countdown -= Time.deltaTime;
            timer.text = "Timer :" + countdown.ToString();
            StartCoroutine(Timer());
        }

        if (countdown < 0 )
        {
            
            gameOver.text = "Game Over";
            //PhotonNetwork.DestroyAll();
            YourScoreIs.text=  score_Text.text;
            playAgain.SetActive(true);
            PhotonNetwork.LocalPlayer.SetScore(0);
        }

    }

    IEnumerator Timer()
    {

        yield return new WaitForSeconds(1f);
        
            if (PhotonNetwork.IsMasterClient)
            {
               
                if (GameObject.FindGameObjectsWithTag("mole").Length == 0 )
                {

                    m_collider = GameObject.FindGameObjectsWithTag("arena")[0].GetComponent<BoxCollider>();   
                    m_Min = m_collider.bounds.min;
                    m_Max = m_collider.bounds.max;

                    Vector3 mol_pos = new Vector3(Random.Range(m_Min.x, m_Max.x), Random.Range(m_Min.y , m_Max.y ), Random.Range(m_Min.z , m_Max.z ));
                    PhotonNetwork.Instantiate("sphere", mol_pos, mole.gameObject.transform.rotation);
                    
                }
            }
     
    }

  public void DisablePlanes()
    {
        foreach(var plane in arPlaneMananger.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }

    public void ScoreCard()
    {
        Player[] pList = PhotonNetwork.PlayerList;

        Player player1 = pList[0];
        Player player2 = pList[1];
        
        if (player1.GetScore() > player2.GetScore())
        {
            score_Text.text = "Score:" + player1.GetScore().ToString();
        }
        else
        {
            score_Text.text = "Score:" + player2.GetScore().ToString();
        }
    }



}
