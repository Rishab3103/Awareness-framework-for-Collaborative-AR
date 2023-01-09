using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;
public class ControlSwitching : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    public string owner;
    private Button transfer_button;
    private Button checkOwner;
    private TextMeshProUGUI owner_cube2;
    private TextMeshProUGUI label_owner;
    private Touch screenTouch;
    float distance = 10;
    Vector3 mousePosition;
    Vector3 objPosition;
    public bool isOwner;
    Player Master;
    Player Client;
    public Toggle toggleOwnership;
    Player new_owner;
    GameObject go;
    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    void Start()
    {
        owner_cube2 = GameObject.Find("Owner_Cube2").GetComponent<TextMeshProUGUI>();
        label_owner = GameObject.Find("Label_Owner").GetComponent<TextMeshProUGUI>();
        owner_cube2.text = PhotonNetwork.MasterClient.NickName;
        label_owner.text = PhotonNetwork.LocalPlayer.NickName;
        Player[] pList = PhotonNetwork.PlayerList;

        for(int i=0; i < pList.Length; i++)
        {
            if(pList[i].NickName==PhotonNetwork.MasterClient.NickName)
            {
                Master = pList[i];
            }
            else
            {
                Client = pList[i];
            }
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            toggleOwnership.gameObject.SetActive(false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        SetMaterial();
        owner_cube2.text = this.photonView.Owner.NickName;
    }

    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
       if (targetView.gameObject != base.photonView.gameObject)
        {
            return;
        }
       
            this.photonView.TransferOwnership(requestingPlayer);
            SetMaterial();
            
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        if (targetView != base.photonView)
        {
            return;
        }
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        throw new System.NotImplementedException();
    }

    public void ToggleOwnership(bool toggle)
    {
       
            if (PhotonNetwork.IsMasterClient)
            {
                if (PhotonNetwork.MasterClient.NickName != gameObject.GetPhotonView().Owner.NickName)
                {
                    this.photonView.RequestOwnership();

                }
                else
                {
                this.photonView.TransferOwnership(Client);
                }
            }
       


    }
    public void SetMaterial()
    {
        if (this.photonView.Owner.NickName == Master.NickName)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        if (this.photonView.Owner.NickName == Client.NickName)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }

    }

}

