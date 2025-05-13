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
            Debug.Log("�� ��������� � ������-�������!");
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("��� ��������� ������. ������� �����..."); 
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 5 });
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("������� ����� � �������!");

        PhotonNetwork.LoadLevel("Game");
    }
}
