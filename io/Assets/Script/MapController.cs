using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour, IOnEventCallback
{
    public PlayarTop PlayarTop;
    public List<PlayerMow> Players;

    public void addPlayers(PlayerMow Player)
    {
        Players.Add(Player);
    }

    void Update()
    {
        PlayarTop.SetTexts(Players);
    }

    public void SentSynkData(Player player)
    {
        SynkData data = new SynkData();

        data.Position = new Vector2Int[Players.Count];
        data.Score = new int[Players.Count];

        PlayerMow[] SortPlayers = Players
            .OrderBy(p => p.photonView.Owner.ActorNumber)
            .ToArray();

        for (int i = 0; i < SortPlayers.Length; i++)
        {
            data.Position[i] = SortPlayers[i].GamePosition;
            data.Score[i] = SortPlayers[i].Score;
        }

        RaiseEventOptions options = new RaiseEventOptions { TargetActors = new[] { player.ActorNumber } };
        SendOptions sentOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(43, data, options, sentOptions);
    }

    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 43:
                var data = (SynkData)photonEvent.CustomData;

                OnSynkDataReceived(data);

                break;
        }
    }

    private void OnSynkDataReceived(SynkData data)
    {
        PlayerMow[] SortPlayers = Players
            .Where(p => !p.photonView.IsMine)
            .OrderBy(p => p.photonView.Owner.ActorNumber)
            .ToArray();
    }
}
