using UnityEngine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;

public class Enviroment : MonoBehaviour
{
    public Bot[] Bot;
    public GameObject BuletPrefab;
    public GameObject CoinPrefab;
    public GameObject[] spuwnPosition;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void OnEnable()
    {
        EventManage.DestroyItem += RespawnItem;
    }
    private void OnDisable()
    {
        EventManage.DestroyItem -= RespawnItem;
    }
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < spuwnPosition.Length; i++)
            {
                GameObject oj = PhotonNetwork.InstantiateRoomObject(Random.value < 0.5f ? BuletPrefab.name : CoinPrefab.name, spuwnPosition[i].transform.position, Quaternion.identity);

                if (!spawnedObjects.Contains(oj))
                {
                    spawnedObjects.Add(oj); 
                    foreach (Bot bot in Bot)
                    {
                        if(bot != null)
                        bot.adTargetObjects(spawnedObjects);
                    }
                }
            }
        }
    }
    public void adPlayers(List<Mow> targeTObjects)
    {
        foreach (Mow t in targeTObjects)
        {
            if (t != null && !spawnedObjects.Contains(t.gameObject))
            {
                spawnedObjects.Add(t.gameObject);
                spawnedObjects.RemoveAll(item => item == null);
                foreach (Bot bot in Bot)
                {
                    if (bot != null)
                        bot.adTargetObjects(spawnedObjects);
                }
            }
        }
    }
    private void RespawnItem(Vector3 position)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        StartCoroutine(WaitAndDoSomething(position));
    }
    IEnumerator WaitAndDoSomething(Vector3 position)
    {
        yield return new WaitForSeconds(8f);
        GameObject oj = PhotonNetwork.InstantiateRoomObject(Random.value < 0.5f ? BuletPrefab.name : CoinPrefab.name, position, Quaternion.identity);

        if (!spawnedObjects.Contains(oj))
        {
            spawnedObjects.Add(oj);
            spawnedObjects.RemoveAll(item => item == null);
            foreach (Bot bot in Bot)
            {
                if (bot != null)
                    bot.adTargetObjects(spawnedObjects);
            }
        }
    }
}
