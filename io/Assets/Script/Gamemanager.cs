using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Linq;

public class Gamemanager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;
    public GameObject Player;
    public MapController mapController;
    public CameraHandler cameraHandler;
    public PlayarTop PlayarTop;
    private List<PlayerMow> allPlayers;
    private bool isLoad = false;
    private void Start()
    {
        StartCoroutine(FindPlayerMowDelayed());
    }
    private void Update()
    {
        if (isLoad == false)
        {
            Vector3 pos = new Vector3(0, 0, 0);
            if (PhotonNetwork.InRoom)
            {
                Player = PhotonNetwork.Instantiate(PlayerPrefab.name, pos, Quaternion.identity);
                Player.GetComponent<PlayerMow>().gamemanager = this;
                cameraHandler.Player = Player;
                isLoad = true;
            }
        }
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
        StartCoroutine(FindPlayerMowDelayed());
    }
    private IEnumerator FindPlayerMowDelayed()
    {
        yield return new WaitForSeconds(0.5f);

        allPlayers = FindObjectsOfType<PlayerMow>().ToList();
    }

    public void SendScore(float score, int ActorNumber)
    {
        photonView.RPC("SyncScoresAndResults", RpcTarget.AllBuffered, score, ActorNumber);
    }
    [PunRPC]
    public void SyncScoresAndResults(float scores, int ActorNumber)
    {
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if(allPlayers[i].OwnerID == ActorNumber)
            {
                allPlayers[i].Score = scores;
            }
        }
        PlayarTop.SetTexts(allPlayers);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if(allPlayers[i].OwnerID == otherPlayer.ActorNumber)
            {
                allPlayers.RemoveAt(i);
            }
        }
    }
}
