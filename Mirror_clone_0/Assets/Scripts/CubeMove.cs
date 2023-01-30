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
    private Button TransferAuthority;
    private Button RevokeAuthority;

    void Start()
    {
        GameObject.FindGameObjectWithTag("cube").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("cube").transform.GetChild(1).gameObject.SetActive(false);
        TransferAuthority = GameObject.Find("Transfer Authority").GetComponent<Button>();
        RevokeAuthority = GameObject.Find("Revoke Authority").GetComponent<Button>();
        TransferAuthority.onClick.AddListener(CmdAssignClientAuthority);
        RevokeAuthority.onClick.AddListener(CmdRevokeAuthority);

    }

   

    // Update is called once per frame
    void Update()
    {

    }

    


    
    [Command]
    public void CmdAssignClientAuthority()
    {
        GameObject.FindGameObjectWithTag("cube").GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);


        GameObject.FindGameObjectWithTag("cube").transform.GetChild(0).gameObject.SetActive(isOwned);
        GameObject.FindGameObjectWithTag("cube").transform.GetChild(1).gameObject.SetActive(!isOwned);

        Debug.Log("Is Owned:" + isOwned);
        RpcToggleChildObjects(isOwned);
    }
    [Command]
    public void CmdRevokeAuthority()
    {
        GameObject.FindGameObjectWithTag("cube").GetComponent<NetworkIdentity>().RemoveClientAuthority();

        GameObject.FindGameObjectWithTag("cube").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("cube").transform.GetChild(1).gameObject.SetActive(false);
        RpcMakeChildObjectsInactive(false);
        Debug.Log("Is Owned Revoke:" + isOwned);
    }
   [ClientRpc]
    private void RpcMakeChildObjectsInactive(bool child1Active)
    {
      
        GameObject.FindGameObjectWithTag("cube").transform.GetChild(0).gameObject.SetActive(child1Active);
        GameObject.FindGameObjectWithTag("cube").transform.GetChild(1).gameObject.SetActive(child1Active);
        Debug.Log("Is Owned Revoke:" + isOwned);
    }

    [ClientRpc]
    private void RpcToggleChildObjects(bool child1Active)
    {

        GameObject.FindGameObjectWithTag("cube").transform.GetChild(0).gameObject.SetActive(child1Active);
        GameObject.FindGameObjectWithTag("cube").transform.GetChild(1).gameObject.SetActive(!child1Active);
        Debug.Log("Is Owned:" + isOwned);
    }
}
