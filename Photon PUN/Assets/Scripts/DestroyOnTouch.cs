using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;

public class DestroyOnTouch : MonoBehaviourPunCallbacks
{
    
    public GameManager game;
    
   
    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("game").GetComponent<GameManager>();
        
     }

   

        // Update is called once per frame
    void Update()
    {
       
        StartCoroutine(Destroy());
        //game.IncreaseScore();

    }

   /* private void OnCollisionEnter(Collision mole)
    {
        if (Input.GetMouseButtonDown(0))
        {
            
                PhotonNetwork.Destroy(gameObject);
                PhotonNetwork.LocalPlayer.AddScore(1);
            Debug.Log("Destroyed");
            

        }
        
        
        
        

    }*/

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        PhotonNetwork.Destroy(gameObject);
        
        
    }

  



}
