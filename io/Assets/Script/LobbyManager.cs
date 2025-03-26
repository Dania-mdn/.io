using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public UI ui;

    void Start()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(1, 20);

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
        if (ui.InputName.text != "")
        {
            PhotonNetwork.NickName = ui.InputName.text;
        }

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
        if (ui.InputName.text != "")
        {
            PhotonNetwork.NickName = ui.InputName.text;
        }

        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Успешно вошли в комнату!");

        PhotonNetwork.LoadLevel("Game");
    }
}
