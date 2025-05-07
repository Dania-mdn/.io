using UnityEngine;
using Photon.Pun;

public class b : MonoBehaviour
{
    private PhotonView photonView;
    private ParticleSystem particleSystem;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        particleSystem = GetComponent<ParticleSystem>();
    }
    public void Play()
    {
        photonView.RPC("DestroyItem", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void DestroyItem()
    {
        if (photonView.IsMine) // Только владелец объекта может вызвать Destroy
        {
            particleSystem.Play();
        }
    }
}
