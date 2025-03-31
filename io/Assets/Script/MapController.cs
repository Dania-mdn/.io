using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
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
}
