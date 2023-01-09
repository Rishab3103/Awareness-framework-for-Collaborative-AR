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
public class TouchDragCube2 : MonoBehaviour
{
    public Transform plane;
    private Transform selection = null;
    private Vector3 dist;
    private TextMeshProUGUI owner_cube2;
    private TextMeshProUGUI local_owner;
    public string scr;
    GameObject go;
    Vector3 mousePosition;
    Vector3 objPosition;
    Vector3 lastPosition;
    private Touch screenTouch;
    // Start is called before the first frame update
    void Start()
    {
        owner_cube2 = GameObject.Find("Owner_Cube2").GetComponent<TextMeshProUGUI>();
        local_owner = GameObject.Find("Label_Owner").GetComponent<TextMeshProUGUI>();
        owner_cube2.text = PhotonNetwork.MasterClient.NickName;
        local_owner.text = PhotonNetwork.LocalPlayer.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.touchCount == 1)
         {
             screenTouch = Input.GetTouch(0);

             Ray ray = Camera.main.ScreenPointToRay(screenTouch.position);
             RaycastHit hit;
             if (Physics.Raycast(ray, out hit, Mathf.Infinity))
             {
                 go = hit.collider.gameObject;
                 if (go == gameObject)
                 {
                     switch (screenTouch.phase)
                     {
                         case TouchPhase.Moved:
                             if (owner_cube2.text == label_owner.text)
                             {
                                 mousePosition = new Vector3(screenTouch.position.x, screenTouch.position.y, 10);
                                 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                                 transform.position = objPosition;
                             }

                             break;
                     }
                 }


             }
         }*/
        if (owner_cube2.text == local_owner.text)
        {
            (this.GetComponent(scr) as MonoBehaviour).enabled = true;
        }
        else
        {
            (this.GetComponent(scr) as MonoBehaviour).enabled = false;
        }

    }
}
