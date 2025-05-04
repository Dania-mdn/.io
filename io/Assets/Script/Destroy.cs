using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public bool isExplousing;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (isExplousing)
        {
            Invoke("destroy", 3);
        }
    }
    public void destroy()
    {
        photonView.RPC("DestroyItem", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void DestroyItem()
    {
        if (photonView.IsMine) // Только владелец объекта может вызвать Destroy
        {
            PhotonNetwork.Destroy(gameObject);
            if (!isExplousing)
            {
                EventManage.DuDestroyItem(transform.position);
            }
        }
    }
}
