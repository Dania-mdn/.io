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
    public string[] NameBot;
    public GameObject[] Bot;
    public GameObject[] SpuwnPosition;
    public Enviroment enviroment;
    public GameObject PlayerPrefab;
    public GameObject Player;
    public CameraHandler cameraHandler;
    public PlayarTop PlayarTop;
    public JoystickPlayerExample joystickPlayerExample;
    private List<Mow> allPlayers;
    private bool isLoad = false;
    private void Start()
    {
        StartCoroutine(FindPlayerMowDelayed());
    }
    private void spuwnDestroyBot()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 1; i < SpuwnPosition.Length; i++)
            {
                if(enviroment.Bot[i] == null)
                {
                    // Защита от выхода за границы массива ботов
                    int botIndex = i % Bot.Length;

                    // Определяем позицию спауна
                    Vector3 spawnPos = SpuwnPosition[i].transform.position;
                    Quaternion spawnRot = SpuwnPosition[i].transform.rotation;

                    // Спавн бота через Photon
                    enviroment.Bot[i] = PhotonNetwork.InstantiateRoomObject(Bot[botIndex].name, spawnPos, spawnRot).GetComponent<Bot>();
                    enviroment.Bot[i].OwnerID = i;
                    enviroment.Bot[i].Nikname(NameBot[i]); 
                    StartCoroutine(FindPlayerMowDelayed());
                }
            }
        }
    }
    private void Update()
    {
        if (isLoad == false)
        {
            Vector3 pos = SpuwnPosition[0].transform.position;
            if (PhotonNetwork.InRoom)
            {
                Player = PhotonNetwork.Instantiate(PlayerPrefab.name, pos, Quaternion.identity);
                PlayerMow playerMow = Player.GetComponent<PlayerMow>();
                playerMow.gamemanager = this;
                playerMow.Nickname = PhotonNetwork.NickName;
                playerMow.joystickPlayerExample = joystickPlayerExample;
                joystickPlayerExample.player = Player.GetComponent<PlayerMow>();
                cameraHandler.Player = Player;
                int skinHID = 0;
                int skinWID = 0;
                if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("skinH") &&
                    PhotonNetwork.LocalPlayer.CustomProperties["skinH"] != null)
                {
                    skinHID = (int)PhotonNetwork.LocalPlayer.CustomProperties["skinH"];
                }
                if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("skinW") &&
                    PhotonNetwork.LocalPlayer.CustomProperties["skinW"] != null)
                {
                    skinWID = (int)PhotonNetwork.LocalPlayer.CustomProperties["skinW"];
                }
                Player.GetComponent<PhotonView>().RPC("SetSkin", RpcTarget.AllBuffered, skinHID, skinWID);

                if (PhotonNetwork.IsMasterClient)
                {
                    for (int i = 1; i < SpuwnPosition.Length; i++)
                    {
                        // Защита от выхода за границы массива ботов
                        int botIndex = i % Bot.Length;

                        // Определяем позицию спауна
                        Vector3 spawnPos = SpuwnPosition[i].transform.position;
                        Quaternion spawnRot = SpuwnPosition[i].transform.rotation;

                        // Спавн бота через Photon
                        enviroment.Bot[i] = PhotonNetwork.Instantiate(Bot[botIndex].name, spawnPos, spawnRot).GetComponent<Bot>();
                        enviroment.Bot[i].OwnerID = i;
                        enviroment.Bot[i].Nikname(NameBot[i]);
                    }
                }

                isLoad = true;
            }
        }
        if (allPlayers != null)
        {
            enviroment.adPlayers(allPlayers);
        }
        spuwnDestroyBot();
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
        yield return new WaitForSeconds(1f);

        allPlayers = FindObjectsOfType<Mow>().ToList();
    }

    public void SendScore(float score, int ActorNumber)
    {
        photonView.RPC("SyncScoresAndResults", RpcTarget.AllBuffered, score, ActorNumber);
    }
    [PunRPC]
    public void SyncScoresAndResults(float scores, int ActorNumber)
    {
        if (allPlayers == null) return;

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
