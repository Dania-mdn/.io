using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public UI ui;

    void Start()
    {
        if (ui.InputName.text == "")
        {
            PhotonNetwork.NickName = "Player" + Random.Range(1, 20);
        }
        else
        {
            PhotonNetwork.NickName = ui.InputName.text;
        }

        Debug.Log("Player`s set to" + PhotonNetwork.NickName);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
    }
    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 12 });
        }
        else
        {
            Debug.Log("Не подключен к Master Server!");
        }
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Успешно вошли в комнату!");

        PhotonNetwork.LoadLevel("Game");
    }
}
