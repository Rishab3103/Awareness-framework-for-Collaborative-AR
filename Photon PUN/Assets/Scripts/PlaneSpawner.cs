using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaneSpawner : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    public GameObject spawnedObject;

    public Pose hitPos;
    public GameObject PlaceablePrefab;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    public Vector3 temp;
    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

   


    // Update is called once per frame
    void Update()
    {
        
        if(!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        if(raycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
             hitPos = s_Hits[0].pose;
            if (spawnedObject == null)
            {
                PlaceablePrefab = PhotonNetwork.Instantiate("arena", hitPos.position, hitPos.rotation);
               /* PhotonNetwork.Instantiate("Hammer1", hitPos.position + new Vector3(0.5f, 0.5f, 0.5f), hitPos.rotation);
                PhotonNetwork.Instantiate("Hammer2", hitPos.position + new Vector3(1f, 1f, 1f), hitPos.rotation);*/
                //PlaceablePrefab.transform.position = hitPos.position;
                spawnedObject = PlaceablePrefab;
                
                //temp = PlaceablePrefab.transform.position;
            }
           /* else
            {
                spawnedObject.transform.position = hitPos.position;
                spawnedObject.transform.rotation = hitPos.rotation;
            }*/
        }


    }
}
