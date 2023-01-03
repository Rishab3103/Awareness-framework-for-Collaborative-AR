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
   
    private Touch touch;
    public Vector3 screen_center;
    public List<GameObject> Players= new List<GameObject>();
    private Player Master;
    private Player Client;
    public string prefabName;
  
    void Start()
    {
        Instance = this;
        //isPresent = false;
        screen_center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        if (isPresent == false)
        {
            PhotonNetwork.Instantiate(this.playerPrefab.name, Vector3.zero, Quaternion.identity, 0);
            isPresent = true;
        }
        
        
        for(int i=0; i< GameObject.FindGameObjectsWithTag(prefabName).Length; i++)
        {
            Players[i] = GameObject.FindGameObjectsWithTag(prefabName)[i];
            if (Players[i].GetPhotonView().Owner == PhotonNetwork.MasterClient)
            {
                Master = Players[i].GetPhotonView().Owner;
            }
            else
            {
                Client= Players[i].GetPhotonView().Owner;
            }
        }

        Debug.Log("Master:" + " " + Master.NickName);
        Debug.Log("Client:" + " " + Client.NickName);


    }

    // Update is called once per frame
    void Update()
    {
        GameObject.FindGameObjectsWithTag(prefabName)[0].GetComponent<MeshRenderer>().material.color = Color.red;
        GameObject.FindGameObjectsWithTag(prefabName)[1].GetComponent<MeshRenderer>().material.color = Color.blue;
        GetLocalPlayer();
    }
    public void GetLocalPlayer()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag(prefabName).Length; i++)
        {
            if (PhotonNetwork.LocalPlayer.NickName == GameObject.FindGameObjectsWithTag(prefabName)[i].GetPhotonView().Owner.NickName)
            {
                GameObject.FindGameObjectsWithTag(prefabName)[i].transform.position = Camera.main.ScreenToWorldPoint(screen_center);
                GameObject.FindGameObjectsWithTag(prefabName)[i].transform.rotation= Camera.main.transform.rotation;


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
