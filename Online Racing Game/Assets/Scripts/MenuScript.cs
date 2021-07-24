using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : Photon.PunBehaviour
{
    public Transform spawn1;
    public GameObject car1;


    public GameObject lobbycam;
    public GameObject lobbyUI;
    public Text status;
    // Start is called before the first frame update
    void Start()
    {
       
        PhotonNetwork.ConnectUsingSettings("1.1");
    }

    // Update is called once per frame
    void Update()
    {
        status.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    public override void OnConnectionFail(DisconnectCause cause)
    {
        base.OnConnectionFail(cause);
    }

    public override void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = false, MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom("roomname", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        lobbycam.SetActive(false);
        lobbyUI.SetActive(false);
        Debug.Log("CarSwitch spawm");
        GameObject mycar = PhotonNetwork.Instantiate( "Car3", spawn1.position, spawn1.rotation, 0);
    }
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        base.OnPhotonPlayerConnected(newPlayer);
    }
    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        base.OnPhotonPlayerDisconnected(otherPlayer);
    }
}
