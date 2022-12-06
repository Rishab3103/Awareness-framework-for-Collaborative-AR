using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun.UtilityScripts;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public static NetworkManager Instance;

    public GameObject playerPrefab;
    public bool isPresent;
    public bool sceneLoad;
    public Camera ARCam;
    public Material materialP1;
    public Material materialP2;
    private Touch touch;
    public Vector3 screen_center;
  
    void Start()
    {
        Instance = this;
        isPresent = false;
        screen_center = new Vector3(Screen.width / 2, Screen.height / 2, 5);
        

    }

    // Update is called once per frame
    void Update()
    {
        screen_center = new Vector3(Screen.width / 2, Screen.height / 2, 5);
        
        if (isPresent == false && GameObject.FindGameObjectsWithTag("dummy").Length != 0)
        {

            PhotonNetwork.Instantiate(this.playerPrefab.name, Vector3.zero, Quaternion.identity, 0);
            isPresent = true;
            GameObject.FindGameObjectsWithTag("hammer1")[0].GetComponent<MeshRenderer>().material = materialP1;
           
            GameObject.FindGameObjectsWithTag("hammer1")[1].GetComponent<MeshRenderer>().material = materialP2;
            
        }
/*
        if (Input.touchCount > 0)
          {
              touch = Input.GetTouch(0);

              if (Input.touchCount > 0)
              {
                  touch = Input.GetTouch(0);

                  // Update the Text on the screen depending on current position of the touch each frame
                  GetLocalPlayer();
              }

          }
*/
        GetLocalPlayer();


    }
    public void GetLocalPlayer()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("hammer1").Length; i++)
        {
            if (PhotonNetwork.LocalPlayer.NickName == GameObject.FindGameObjectsWithTag("hammer1")[i].GetPhotonView().Owner.NickName)
            {
                GameObject.FindGameObjectsWithTag("hammer1")[i].transform.position = Camera.main.ScreenToWorldPoint(screen_center);


            }
        }
    }

   
    


public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
        //PhotonNetwork.DestroyAll();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.DestroyAll();
    }

    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);


    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena();

        }

    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena();
        }
    }


    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
