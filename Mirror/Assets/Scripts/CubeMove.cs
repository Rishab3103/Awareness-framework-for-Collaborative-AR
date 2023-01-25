using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
public class CubeMove : NetworkBehaviour
{
    // Start is called before the first frame update
    Vector3 position;
    [SerializeField] private Transform objectToSync;

    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            CmdAssignClientAuthority();
            
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            CmdAssignServerAuthority();
        }
      if(isServer)
        {
            MoveCubeOnClient();
        }
       

 
       
    }
    [Command]
    public void MoveCubeOnClient()
    {
        HandleMovement();
    }
    

    [ClientRpc]
    public void HandleMovement()
    {
        if(isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
            GameObject.FindGameObjectWithTag("cube").transform.position = GameObject.FindGameObjectWithTag("cube").transform.position + movement;
            Debug.Log("MoveClient");
        }
     

        

    }
    
    [Command]
    public void CmdAssignClientAuthority()
    {
        GameObject.FindGameObjectWithTag("cube").GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);

    }

    public void CmdAssignServerAuthority()
    {
        GameObject.FindGameObjectWithTag("cube").GetComponent<NetworkIdentity>().RemoveClientAuthority();

    }
}
