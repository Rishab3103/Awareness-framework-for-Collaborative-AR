using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using Photon.Realtime;
using TMPro;
using Photon.Pun.UtilityScripts;

public class Gaze : MonoBehaviourPun
{
 
   
    Player player1;
    Player player2;
    private TextMeshProUGUI viewer_label;
    GameObject pl1_Go;
    GameObject pl2_Go;
    GameObject Master;
    GameObject Client;
    public string PrefabName;
    bool isMasterViewing;
    bool isClientViewing;
    // Start is called before the first frame update
    void Start()
    {
        Player[] pList = PhotonNetwork.PlayerList;
        player1 = pList[0];
        player2 = pList[1];
        
        viewer_label = GameObject.Find("Viewer_Label").GetComponent<TextMeshProUGUI>();

        
    }

    // Update is called once per frame
    void Update()
    {
        /*Master = GameObject.FindGameObjectsWithTag("hammer1")[0];
        Client = GameObject.FindGameObjectsWithTag("hammer1")[1];*/
        for(int i=0; i< GameObject.FindGameObjectsWithTag(PrefabName).Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag(PrefabName)[i].GetPhotonView().Owner == PhotonNetwork.MasterClient)
            {
                Master = GameObject.FindGameObjectsWithTag(PrefabName)[i];
            }
            else
            {
                Client= GameObject.FindGameObjectsWithTag(PrefabName)[i];
            }
        }
        GazeDetection();
        Debug.Log("Client Viewing:"+ isClientViewing);
        Debug.Log("Master Viewing:"+ isMasterViewing);
        Debug.Log("Master:" + Master.GetPhotonView().Owner.NickName);
        Debug.Log("Client:" + Client.GetPhotonView().Owner.NickName);
    }
    public void GazeDetection()
    {


        if (Physics.Raycast(Master.transform.position, Master.transform.forward, out RaycastHit hit_Master))
        {
            GameObject go_master = hit_Master.collider.gameObject;
            if (go_master.CompareTag("cube"))
            {
                isMasterViewing = true;
                if (isClientViewing)
                {
                    viewer_label.text = "Viewer is " + Master.GetPhotonView().Owner.NickName + " " + "and" + " " + Client.GetPhotonView().Owner.NickName;
                }
                else
                {
                    Debug.DrawRay(Master.transform.position, Master.transform.TransformDirection(Vector3.forward) * hit_Master.distance, Color.yellow);
                    viewer_label.text = "Viewer is " + Master.GetPhotonView().Owner.NickName;
                }

               

            }
            else
            {
                viewer_label.text = " ";
                isMasterViewing = false;
            }

        }

        if (Physics.Raycast(Client.transform.position, Client.transform.forward, out RaycastHit hit_Client))
        {
            GameObject go_client = hit_Client.collider.gameObject;
            if (go_client.CompareTag("cube"))
            {
                isClientViewing = true;
                if (isMasterViewing)
                {
                    viewer_label.text = "Viewer is " + Master.GetPhotonView().Owner.NickName + " " + "and" + " " + Client.GetPhotonView().Owner.NickName;

                }
                else
                {
                    Debug.DrawRay(Client.transform.position, Client.transform.TransformDirection(Vector3.forward) * hit_Client.distance, Color.yellow);
                    viewer_label.text = "Viewer is " + Client.GetPhotonView().Owner.NickName;
                }
            }
            else
            {
                viewer_label.text = " ";
                isClientViewing = false;
            }

        }





    }



}
