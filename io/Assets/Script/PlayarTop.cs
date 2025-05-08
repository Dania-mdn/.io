using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public class PlayarTop : MonoBehaviourPunCallbacks
{
    private List<Mow> players;
    private int totalChildren;
    public UIGame uigame;

    private void Start()
    {
        totalChildren = transform.childCount;

        foreach (var text in GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.text = "";
        }
    }

    public void SetTexts(List<Mow> Players)
    {
        players = Players;
    }
    private void Update()
    {
        if (players == null) return;

        var validPlayers = players
    .Where(p => p != null && p.gameObject != null)
    .OrderByDescending(p => p.Score)
    .ToList();

        for (int i = 0; i < validPlayers.Count; i++)
        {
            if (validPlayers[i].GetComponent<PlayerMow>() != null && validPlayers[i].photonView.Owner == PhotonNetwork.LocalPlayer)
            {
                uigame.PositionInTop = i + 1;
                break;
            }
        }

        // Выводим только топ-5
        Mow[] top = validPlayers.Take(5).ToArray();

        for (int i = 0; i < top.Length; i++)
        {
            if (top[i].GetComponent<PlayerMow>() != null)
            {
                transform.GetChild(i).GetComponent<TextMeshProUGUI>().text =
                    (i + 1) + ". " + top[i].photonView.Owner.NickName;
            }
            else
            {
                transform.GetChild(i).GetComponent<TextMeshProUGUI>().text =
                    (i + 1) + ". " + top[i].Nickname;
            }
            transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                top[i].Score.ToString("F0");
        }

        for (int j = top.Length; j < totalChildren; j++)
        {
            transform.GetChild(j).GetComponent<TextMeshProUGUI>().text = "";
            transform.GetChild(j).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
