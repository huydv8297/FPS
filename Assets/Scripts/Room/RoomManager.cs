using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RoomManager : Photon.Monobehaviour
{

    public GameObject roomPrefab;
    public string gameVersion = "1.0";
    void Awake()
    {
       PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start () {
        Connect();
    }

    public void Connect()
    {
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
