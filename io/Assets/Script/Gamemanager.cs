using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Gamemanager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;
    public GameObject Player;
    public MapController mapController;
    public CameraHandler cameraHandler;
    private bool isLoad = false;

    private void Start()
    {
        //PhotonPeer.RegisterType(typeof(SynkData), 243, SynkData.Serialyze, SynkData.Deserialyze);
    }
    private void Update()
    {
        if (isLoad == false)
        {
            Vector3 pos = new Vector3(0, 5, 0);
            if (PhotonNetwork.InRoom)
            {
                Player = PhotonNetwork.Instantiate(PlayerPrefab.name, pos, Quaternion.identity);
                mapController.addPlayers(Player.GetComponent<PlayerMow>());
                cameraHandler.Player = Player;
                isLoad = true;
            }
        }
        Player.GetComponent<PlayerMow>().Score++;
    }

    public void LeftRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            newPlayer.ph
            //mapController.addPlayers(newPlayer.G .GetComponent<PlayerMow>());
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //Debug.LogFormat("Player left room" + otherPlayer.NickName, otherPlayer.NickName);
    }
}
