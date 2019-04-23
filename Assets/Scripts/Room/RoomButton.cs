using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour {

    public string roomName;
    public int countPlayer;
    public Text countPlayerUI;

   
	void Start () {
		
	}
	
	public void SetInformation(string name, int count)
    {
        roomName = name;
        countPlayer = count;
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
