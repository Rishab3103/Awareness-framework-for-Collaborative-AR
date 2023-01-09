using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class ImageTracker : MonoBehaviourPun
{


    public ARTrackedImageManager m_ImageManager;
    public XRReferenceImageLibrary m_ImageLibrary;
    //public GameObject arena;
    public Vector3 position;
    public Quaternion rotation;
    public bool addAnchor;
    
    public GameObject arena;
    public int m_NumberOfTrackedImages;
   // private GameObject spawnedObject;
   // public GameObject anchor_object;
    //private ARAnchor anchor_create;
    public int counter;
   
    private void Awake()
    {
        m_ImageManager = FindObjectOfType<ARTrackedImageManager>();
       
    }

    public void OnEnable()
    {
        
        m_ImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        m_ImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (ARTrackedImage img in obj.updated)
        {

            arena.transform.position = img.transform.position;
            arena.transform.position = img.transform.position;

            position = img.transform.position;
            rotation = img.transform.rotation;
            
            

                
            
            
           

            if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "P1Active", 1 } });
                    
                }
                else
                {
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "P2Active", 1 } });
                }

               
                
            
            

        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
