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
public class ControlSwitching3Players : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
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
    Player Client1;
    Player Client2;
    public Toggle toggleOwnershipClient1;
    public Toggle toggleOwnershipClient2;
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
        Master = pList[0];
        Client1 = pList[1];
        Client2= pList[2];

        if (!PhotonNetwork.IsMasterClient)
        {
            toggleOwnershipClient1.gameObject.SetActive(false);
            toggleOwnershipClient2.gameObject.SetActive(false);
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

    public void ToggleOwnershipClient1(bool toggle)
    {

        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.MasterClient.NickName != gameObject.GetPhotonView().Owner.NickName)
            {
                this.photonView.RequestOwnership();

            }
            else
            {
                this.photonView.TransferOwnership(Client1);
            }
        }



    }

    public void ToggleOwnershipClient2(bool toggle)
    {

        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.MasterClient.NickName != gameObject.GetPhotonView().Owner.NickName)
            {
                this.photonView.RequestOwnership();

            }
            else
            {
                this.photonView.TransferOwnership(Client2);
            }
        }



    }
    public void SetMaterial()
    {
        if (this.photonView.Owner.NickName == Master.NickName)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        if (this.photonView.Owner.NickName == Client1.NickName)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        if (this.photonView.Owner.NickName == Client2.NickName)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.green;
        }

    }
}
