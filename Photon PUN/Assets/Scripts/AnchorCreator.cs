using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARCore;
[RequireComponent(typeof(ARAnchorManager))]

public class AnchorCreator : MonoBehaviour
{
   
    public ARAnchorManager m_AnchorManager;
    
    
    public GameObject arena;

 

 
    // Start is called before the first frame update
    void Start()
    {
        
        m_AnchorManager = GetComponent<ARAnchorManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (arena.GetComponent<ARAnchor>() == null)
        {
            ARAnchor anchor = arena.AddComponent<ARAnchor>();
            Instantiate(arena, anchor.transform);
        }

    }


}
