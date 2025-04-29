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
        photonView.RPC("DestroyItem", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void DestroyItem()
    {
        Destroy(gameObject);
    }
}
