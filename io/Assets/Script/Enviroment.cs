using UnityEngine;
using Photon.Pun;
using System.Collections;

public class Enviroment : MonoBehaviour
{
    public GameObject BuletPrefab;
    public GameObject CoinPrefab;
    public GameObject[] spuwnPosition;

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
                PhotonNetwork.Instantiate(Random.value < 0.5f ? BuletPrefab.name : CoinPrefab.name, spuwnPosition[i].transform.position, Quaternion.identity);
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
        GameObject oj = PhotonNetwork.Instantiate(Random.value < 0.5f ? BuletPrefab.name : CoinPrefab.name, position, Quaternion.identity);
    }
}
