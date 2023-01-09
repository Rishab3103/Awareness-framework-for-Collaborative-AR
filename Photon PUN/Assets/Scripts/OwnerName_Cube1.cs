using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class OwnerName_Cube1 : MonoBehaviour
{
    const string playerNamePrefKey = "PlayerName";
    
    public OwnershipTransfer Transfer;
    private TMP_InputField owner_cube1;
    // Start is called before the first frame update
    void Start()
    {
        owner_cube1 = this.GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        owner_cube1.text = Transfer.owner;
    }

   
}
