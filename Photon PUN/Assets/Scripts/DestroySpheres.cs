using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
public class DestroySpheres : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "mole")
        {
            if (photonView.IsMine)
            {
                
                PhotonNetwork.Destroy(collision.gameObject);
                PhotonNetwork.LocalPlayer.AddScore(1);
            }
        }
    }
}
