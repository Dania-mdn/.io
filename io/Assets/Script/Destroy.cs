using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    public void destroy()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
