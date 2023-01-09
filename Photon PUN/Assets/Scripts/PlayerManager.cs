using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;
    // Start is called before the first frame update
    void Awake()
    {
      /*  if(photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);*/
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
