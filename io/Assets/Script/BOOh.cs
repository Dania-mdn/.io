using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOOh : MonoBehaviour
{
    public PhotonView photonView;

    private float coldawn = 5;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

    }

    private void Update()
    {
        if(coldawn > 0)
        {
            coldawn = coldawn - 1 * Time.deltaTime;
        }
        else
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
