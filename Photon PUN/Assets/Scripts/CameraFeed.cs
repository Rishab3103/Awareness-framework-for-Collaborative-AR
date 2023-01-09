using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Photon.Pun;
using Photon.Realtime;


public class CameraFeed : MonoBehaviourPun
{

    public RawImage rawImage;
    public ARCameraBackground camBack;
    private ARCameraBackground controllerCamBack;
    Player Master;
    Player Client;
    private void Start()
    {
        rawImage = GameObject.Find("RawImage").GetComponent<RawImage>();
        camBack = GetComponent<ARCameraBackground>();
        Player[] pList = PhotonNetwork.PlayerList;

        for (int i = 0; i < pList.Length; i++)
        {
            if (pList[i].NickName == PhotonNetwork.MasterClient.NickName)
            {
                Master = pList[i];
            }
            else
            {
                Client = pList[i];
            }
        }

    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            controllerCamBack = camBack;
            rawImage.gameObject.SetActive(false);
            
        }
        photonView.RPC("SendCameraFeed", Client, controllerCamBack);
    }


    [PunRPC]
    void SendCameraFeed(ARCameraBackground cameraBackground)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            rawImage.material = cameraBackground.material;
        }
    }
}
