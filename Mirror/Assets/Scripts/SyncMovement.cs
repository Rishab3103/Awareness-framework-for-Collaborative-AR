using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SyncMovement : NetworkBehaviour
{
    // Start is called before the first frame update


    /*
        private Vector3 targetPosition;
        private Vector3 targetPosServer;
        void Update()
        {
            if (isOwned)
            {
                // Move the object based on input
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");
                Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
                transform.position = transform.position + movement;

                // Send the new position to the server
                CmdSyncPosition(transform.position);
            }


            else
            {
                // Interpolate the object's position on the client
                transform.position = targetPosition;
            }
        }
        public override void OnStopAuthority()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
            transform.position = transform.position + movement;
            CmdSyncPositionServer(transform.position);
        }

        // Command sent from the client to the server to update the object's position
        [Command]
        void CmdSyncPosition(Vector3 position)
        {
            targetPosition = position;
        }

        [Command]
        void CmdSyncPositionServer(Vector3 position)
        {
            targetPosServer = position;
        }*/


    private GameObject syncedObject;

    // Authority variables
    private bool isOwner = false;
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private void Start()
    {
        syncedObject = this.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (isOwner)
        {
            // Send position and rotation updates to the server
            if (syncedObject.transform.position != lastPosition)
            {
                lastPosition = syncedObject.transform.position;
                CmdSendPosition(lastPosition);
            }
            if (syncedObject.transform.rotation != lastRotation)
            {
                lastRotation = syncedObject.transform.rotation;
                CmdSendRotation(lastRotation);
            }
        }
        else
        {
            // Update the position and rotation of the synced object based on the server's values
            syncedObject.transform.position = lastPosition;
            syncedObject.transform.rotation = lastRotation;
        }
    }

    // Called when authority is transferred to this object
    public override void OnStartAuthority()
    {
        isOwner = true;
    }

    // Called when authority is removed from this object
    public override void OnStopAuthority()
    {
        isOwner = false;
    }

    // Command to send position updates to the server
    [Command]
    void CmdSendPosition(Vector3 position)
    {
        lastPosition = position;
        RpcUpdatePosition(position);
    }

    // Command to send rotation updates to the server
    [Command]
    void CmdSendRotation(Quaternion rotation)
    {
        lastRotation = rotation;
        RpcUpdateRotation(rotation);
    }

    // RPC to update position on clients
    [ClientRpc]
    void RpcUpdatePosition(Vector3 position)
    {
        lastPosition = position;
        if (!isOwner)
        {
            syncedObject.transform.position = position;
        }
    }

    // RPC to update rotation on clients
    [ClientRpc]
    void RpcUpdateRotation(Quaternion rotation)
    {
        lastRotation = rotation;
        if (!isOwner)
        {
            syncedObject.transform.rotation = rotation;
        }
    }
}
