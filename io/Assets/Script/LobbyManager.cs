using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public UI ui;

    void Start()
    {
        ui.Close.SetActive(true);

        PhotonNetwork.NickName = "Player" + Random.Range(1, 998);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }
    public void SetUsername(string username)
    {
        ui.name.text = username.ToString();
        PhotonNetwork.NickName = username;
    }
    public override void OnConnectedToMaster()
    {
        ui.Close.SetActive(false);
        Debug.Log("Connected to Master");
    }
    public void Play()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log("Не подключен к мастер-серверу!");
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Нет доступных комнат. Создаем новую..."); 
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 5 });
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Успешно вошли в комнату!");

        PhotonNetwork.LoadLevel("Game");
    }
}
