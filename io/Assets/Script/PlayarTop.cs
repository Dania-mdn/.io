using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayarTop : MonoBehaviour
{
    private void Start()
    {
        foreach (var text in GetComponentsInChildren<Text>())
        {
            text.text = "";
        }
    }

    public void SetTexts(List<PlayerMow> Players)
    {
        PlayerMow[] top = Players
            .OrderByDescending(p => p.Score)
            .Take(5)
            .ToArray();

        for (int i = 0; i < top.Length; i++)
        {
            transform.GetChild(i).GetComponent<TextMeshProUGUI>().text =
                (i + 1) + ". " + top[i].GetComponent<PhotonView>().Owner.NickName;
            transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = top[i].Score.ToString();
        }
    }
}
