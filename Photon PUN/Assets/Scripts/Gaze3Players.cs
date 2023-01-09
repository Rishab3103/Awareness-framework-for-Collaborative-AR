using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using Photon.Realtime;
using TMPro;
using Photon.Pun.UtilityScripts;
public class Gaze3Players : MonoBehaviour
{
    private TextMeshProUGUI viewer_label;
    GameObject pl1_Go;
    GameObject pl2_Go;
    GameObject Master;
    GameObject Client1;
    GameObject Client2;
    public string PrefabName;
    bool isMasterViewing;
    bool isClientViewing;
    RaycastHit hit_Master;
    RaycastHit hit_Client1;
    RaycastHit hit_Client2;
    GameObject go_master;
    GameObject go_client1;
    GameObject go_client2;
    public List<string> prefabNameTag;

    private Transform child;
    // Start is called before the first frame update
    void Start()
    { 
        viewer_label = GameObject.Find("Viewer_Label").GetComponent<TextMeshProUGUI>();
      
    }

    // Update is called once per frame
    void Update()
    {
        /*Master = GameObject.FindGameObjectsWithTag("hammer1")[0];
        Client = GameObject.FindGameObjectsWithTag("hammer1")[1];*/

        for (int i = 0; i < GameObject.FindGameObjectsWithTag(PrefabName).Length; i++)
        {
            if (i==0)
            {
                Master = GameObject.FindGameObjectsWithTag(PrefabName)[i];
            }
            if(i==1)
            {
                Client1 = GameObject.FindGameObjectsWithTag(PrefabName)[i];
            }
            if (i == 2)
            {
                Client2 = GameObject.FindGameObjectsWithTag(PrefabName)[i];
            }
        }
        GazeDetection();
    }
    public void GazeDetection()
    {

        for (int i = 0; i < prefabNameTag.Count; i++)
        {
            if (Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && !Physics.Raycast(Client1.transform.position, Client1.transform.forward, out hit_Client1) && !Physics.Raycast(Client2.transform.position, Client2.transform.forward, out hit_Client2))
            {
                go_master = hit_Master.collider.gameObject;
                go_client1 = null;
                go_client2 = null;
                if (go_master.CompareTag(prefabNameTag[i]))
                {
                    GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_master.GetPhotonView().Owner.NickName;
                }
                else
                {
                    GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "No viewer";

                }
            }

            if (!Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && Physics.Raycast(Client1.transform.position, Client1.transform.forward, out hit_Client1) && !Physics.Raycast(Client2.transform.position, Client2.transform.forward, out hit_Client2))
            {
                
                go_master = null;
                go_client1 = hit_Client1.collider.gameObject;
                go_client2 = null;
                if (go_client1.CompareTag(prefabNameTag[i]))
                {
                    GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_client1.GetPhotonView().Owner.NickName;
                }
                else
                {
                    GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "No viewer";
                }
            }
            if (!Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && !Physics.Raycast(Client1.transform.position, Client1.transform.forward, out hit_Client1) && Physics.Raycast(Client2.transform.position, Client2.transform.forward, out hit_Client2))
            {

                go_master = null;
                go_client1 = null;
                go_client2 = hit_Client2.collider.gameObject;
                if (go_client2.CompareTag(prefabNameTag[i]))
                {
                    GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_client2.GetPhotonView().Owner.NickName;
                }
                else
                {
                    GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "No viewer";
                }
            }
            if (!Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && !Physics.Raycast(Client1.transform.position, Client1.transform.forward, out hit_Client1)&&!Physics.Raycast(Client2.transform.position, Client2.transform.forward, out hit_Client2))
            {

                GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "No viewer";
            }
            if (!Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && Physics.Raycast(Client1.transform.position, Client1.transform.forward, out hit_Client1) && Physics.Raycast(Client2.transform.position, Client2.transform.forward, out hit_Client2))
            {
                go_client1 = hit_Client1.collider.gameObject;
                go_client2 = hit_Client2.collider.gameObject;
                if (go_client1.name== go_client2.name)
                {
                    GameObject.FindGameObjectWithTag(go_client1.tag).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_client1.GetPhotonView().Owner.NickName + "and" + go_client2.GetPhotonView().Owner.NickName;
                }
                else
                {
                    for (int j = 0; j < prefabNameTag.Count; j++)
                    {
                        if (go_client1.CompareTag(prefabNameTag[j]))
                        {
                            GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_client1.GetPhotonView().Owner.NickName;
                        }
                        if (go_client2.CompareTag(prefabNameTag[j]))
                        {
                            GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_client2.GetPhotonView().Owner.NickName;
                        }

                    }


                }
            }

            if (Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && !Physics.Raycast(Client1.transform.position, Client1.transform.forward, out hit_Client1) && Physics.Raycast(Client2.transform.position, Client2.transform.forward, out hit_Client2))
            {
                go_master = hit_Master.collider.gameObject;
                go_client1 = null;
                go_client2 = hit_Client2.collider.gameObject;
                if (go_master.name == go_client2.name)
                {
                    GameObject.FindGameObjectWithTag(go_master.tag).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_master.GetPhotonView().Owner.NickName + "and" + go_client2.GetPhotonView().Owner.NickName;
                }
                else
                {
                    for (int j = 0; j < prefabNameTag.Count; j++)
                    {
                        if (go_master.CompareTag(prefabNameTag[j]))
                        {
                            GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_master.GetPhotonView().Owner.NickName;
                        }
                        if (go_client2.CompareTag(prefabNameTag[j]))
                        {
                            GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_client2.GetPhotonView().Owner;
                        }

                    }


                }
            }
            if (Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && Physics.Raycast(Client1.transform.position, Client1.transform.forward, out hit_Client1) && !Physics.Raycast(Client2.transform.position, Client2.transform.forward, out hit_Client2))
            {
                go_master = hit_Master.collider.gameObject;
                go_client1 = hit_Client1.collider.gameObject;
                go_client2 = null;
                if (go_client1.name == go_master.name)
                {
                    GameObject.FindGameObjectWithTag(go_client1.tag).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_client1.GetPhotonView().Owner.NickName + "and" + go_master.GetPhotonView().Owner.NickName;
                }
                else
                {
                    for (int j = 0; j < prefabNameTag.Count; j++)
                    {
                        if (go_client1.CompareTag(prefabNameTag[j]))
                        {
                            GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_client1.GetPhotonView().Owner.NickName;
                        }
                        if (go_master.CompareTag(prefabNameTag[j]))
                        {
                            GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_master.GetPhotonView().Owner.NickName;
                        }

                    }


                }
            }
            if (Physics.Raycast(Master.transform.position, Master.transform.forward, out hit_Master) && Physics.Raycast(Client1.transform.position, Client1.transform.forward, out hit_Client1) && Physics.Raycast(Client2.transform.position, Client2.transform.forward, out hit_Client2))
            {
                go_master = hit_Master.collider.gameObject;
                go_client1 = hit_Client1.collider.gameObject;
                go_client2 = hit_Client2.collider.gameObject;
                if ((go_master.name == go_client1.name) && (go_client1.name == go_client2.name) && (go_master.name == go_client2.name))
                {
                    GameObject.FindGameObjectWithTag(go_master.tag).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_master.GetPhotonView().Owner.NickName + "and" + go_client1.GetPhotonView().Owner.NickName + "and" + go_client2.GetPhotonView().Owner.NickName;
                }
                else
                {
                    for (int j = 0; j < prefabNameTag.Count; j++)
                    {
                        if (go_master.CompareTag(prefabNameTag[j]))
                        {
                            GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_master.GetPhotonView().Owner.NickName;
                        }
                        if (go_client1.CompareTag(prefabNameTag[j]))
                        {
                            GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_client1.GetPhotonView().Owner.NickName;
                        }
                        if (go_client2.CompareTag(prefabNameTag[j]))
                        {
                            GameObject.FindGameObjectWithTag(prefabNameTag[i]).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The Viewer is " + " " + go_client2.GetPhotonView().Owner.NickName;
                        }

                    }


                }
            }

        }
    }
}
