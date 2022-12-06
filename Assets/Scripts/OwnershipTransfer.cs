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
using Lean;
public class OwnershipTransfer : MonoBehaviourPun, IPunOwnershipCallbacks
{

    
    public string owner;
    private Button transfer_button;
    private Button checkOwner;
    private TextMeshProUGUI owner_cube1;
    private TextMeshProUGUI label_owner;
    private Touch screenTouch;
    float distance = 10;
    Vector3 mousePosition;
    Vector3 objPosition;
    public bool isOwner;
    Player player1;
    Player player2;
    
    Player new_owner;
    /*bool isP1;
    bool isP2;
    PhotonView pv1;
    PhotonView pv2;*/
    
    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    void Start()
    {
        owner_cube1 = GameObject.Find("Owner_Cube1").GetComponent<TextMeshProUGUI>();
        label_owner = GameObject.Find("Label_Owner").GetComponent<TextMeshProUGUI>();
        owner_cube1.text = PhotonNetwork.MasterClient.NickName;
        label_owner.text = PhotonNetwork.LocalPlayer.NickName;
        Player[] pList = PhotonNetwork.PlayerList;

        player1 = pList[0];
        player2 = pList[1];






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
        owner = requestingPlayer.NickName;
        owner_cube1.text = owner;
        new_owner = requestingPlayer;

        
        
        



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

    // Start is called before the first frame update


    public void TransferOwner()
    {
        this.photonView.RequestOwnership();
       

    }

/*   public void OnMouseDrag()
    {
        
        if(owner_cube1.text==label_owner.text)
        {
            
            mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
            
            
        }
        transform.position = objPosition;

    }*/

    /*public void CheckOwner()
    {
        label_owner.text = PhotonNetwork.LocalPlayer.NickName;
    }*/
/*    public void TouchDrag()
    {
        if (Input.touchCount == 1)
        {
            screenTouch = Input.GetTouch(0);

            if (screenTouch.phase == TouchPhase.Moved)
            {
                if (owner_cube1.text == label_owner.text)
                {
                    
                    mousePosition = new Vector3(screenTouch.position.x, screenTouch.position.y, distance);
                    objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    transform.position = objPosition;
                }
            }
        }
    }*/



    public void SetMaterial()
    {
        if (this.photonView.Owner.NickName == player1.NickName)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        if (this.photonView.Owner.NickName == player2.NickName)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }

    }
    public void Update()
    {
       
        SetMaterial();
    }

}
