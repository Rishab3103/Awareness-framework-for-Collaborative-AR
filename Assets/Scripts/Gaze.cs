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
    RaycastHit hit_Master;
    RaycastHit hit_Client;
    GameObject go_master;
    GameObject go_client;
    public List<string> prefabNameTag;

    
    private Transform child;
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
        
        for (int i=0; i< GameObject.FindGameObjectsWithTag(PrefabName).Length; i++)
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
        Debug.Log("Number of tags" + prefabNameTag.Count);
    }
    public void GazeDetection()
    {

        for(int i=0; i<prefabNameTag.Count; i++)
        {
            if (Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && !Physics.Raycast(Client.transform.position, Client.transform.forward, out hit_Client))
            {
                Debug.DrawRay(Master.transform.position, Master.transform.TransformDirection(Vector3.forward) * hit_Master.distance, Color.red);
                Debug.DrawRay(Client.transform.position, Client.transform.TransformDirection(Vector3.forward) * hit_Client.distance, Color.blue);
                go_master = hit_Master.collider.gameObject;
                go_client = null;
                if (go_master.CompareTag(prefabNameTag[i]))
                {
                    GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
                }
                else
                {
                    GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;

                }
            }

            if (!Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && Physics.Raycast(Client.transform.position, Client.transform.forward, out hit_Client))
            {
                Debug.DrawRay(Master.transform.position, Master.transform.TransformDirection(Vector3.forward) * hit_Master.distance, Color.red);
                Debug.DrawRay(Client.transform.position, Client.transform.TransformDirection(Vector3.forward) * hit_Client.distance, Color.blue);
                go_master = null;
                go_client = hit_Client.collider.gameObject;
                if (go_client.CompareTag(prefabNameTag[i]))
                {
                    GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
                }
                else
                {
                    GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
            if (Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && Physics.Raycast(Client.transform.position, Client.transform.forward, out hit_Client))
            {
                Debug.DrawRay(Master.transform.position, Master.transform.TransformDirection(Vector3.forward) * hit_Master.distance, Color.red);
                Debug.DrawRay(Client.transform.position, Client.transform.TransformDirection(Vector3.forward) * hit_Client.distance, Color.blue);
                go_master = hit_Master.collider.gameObject;
                go_client = hit_Client.collider.gameObject;
                if (go_master == go_client)
                {
                    GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.yellow;

                }
                else
                {
                    for (int j = 0; j < prefabNameTag.Count; j++)
                    {
                        if (go_master.CompareTag(prefabNameTag[j]))
                        {
                            go_master.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                        if (go_client.CompareTag(prefabNameTag[j]))
                        {
                            go_client.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
                        }
                        
                    }
                   
       
                }
            }
            if (!Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && !Physics.Raycast(Client.transform.position, Client.transform.forward, out hit_Client))
            {
                Debug.DrawRay(Master.transform.position, Master.transform.TransformDirection(Vector3.forward) * hit_Master.distance, Color.red);
                Debug.DrawRay(Client.transform.position, Client.transform.TransformDirection(Vector3.forward) * hit_Client.distance, Color.blue);
                GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
            }

        }
    }



        /*if(Physics.Raycast(Master.transform.position,Master.transform.forward, out hit_Master) && !Physics.Raycast(Client.transform.position, Client.transform.forward, out hit_Client))
        {
            go_master = hit_Master.collider.gameObject;
            go_client = null;
            if (go_master.CompareTag("cube1"))
            {
                    
                    viewer_label.text =  Master.GetPhotonView().Owner.NickName + "is viewing Cube1 ";

            }
            else
            {
                viewer_label.text = " ";
                
            }
        }

        if (!Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && Physics.Raycast(Client.transform.position, Client.transform.forward, out hit_Client))
        {
            go_master = null;
            go_client = hit_Client.collider.gameObject;
            if (go_client.CompareTag("cube1"))
            {
                
                viewer_label.text =  Client.GetPhotonView().Owner.NickName + "is viewing Cube1 ";

            }
            else
            {
                viewer_label.text = " ";
                
            }
        }
        if(Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && Physics.Raycast(Client.transform.position, Client.transform.forward, out hit_Client))
        {
            go_master = hit_Master.collider.gameObject;
            go_client = hit_Client.collider.gameObject;
            if (go_master.CompareTag("cube1") && go_client.CompareTag("cube1"))
            {
               // Debug.DrawRay(Master.transform.position, Master.transform.TransformDirection(Vector3.forward) * hit_Master.distance, Color.yellow);
                viewer_label.text =  Master.GetPhotonView().Owner.NickName + " " + "and" + " " + Client.GetPhotonView().Owner.NickName + "are viewing Cube1 ";

            }
            else
            {
                viewer_label.text = " ";
                
            }
        }
        if(!Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && !Physics.Raycast(Client.transform.position, Client.transform.forward, out hit_Client))
        {
            viewer_label.text = " ";
        }

    }*/



}
