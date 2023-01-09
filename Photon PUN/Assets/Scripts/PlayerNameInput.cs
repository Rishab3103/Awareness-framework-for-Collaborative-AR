using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{

    const string playerNamePrefKey = "PlayerName";
    // Start is called before the first frame update
    void Start()
    {


        string defaultName = string.Empty;
        TMP_InputField _inputField = this.GetComponent<TMP_InputField>();
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }


        PhotonNetwork.NickName = defaultName;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayerName(string value)
    {
        // #Important
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;


        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
}
