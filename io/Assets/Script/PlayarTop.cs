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
    private List<PlayerMow> players;
    private int totalChildren;

    private void Start()
    {
        totalChildren = transform.childCount;

        foreach (var text in GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.text = "";
        }
    }

    public void SetTexts(List<PlayerMow> Players)
    {
        players = Players;
    }
    private void Update()
    {
        if (players == null) return;

        PlayerMow[] top = players
              .OrderByDescending(p => p.Score)
              .Take(5)
              .ToArray();

        for (int i = 0; i < top.Length; i++)
        {
            transform.GetChild(i).GetComponent<TextMeshProUGUI>().text =
                (i + 1) + ". " + top[i].GetComponent<PhotonView>().Owner.NickName;
            transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = top[i].Score.ToString("F0");
        }

        for (int j = top.Length; j < totalChildren; j++)
        {
            transform.GetChild(j).GetComponent<TextMeshProUGUI>().text = "";
            transform.GetChild(j).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
