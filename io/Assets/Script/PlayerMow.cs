using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMow : MonoBehaviour
{
    private PhotonView photonView;

    private RaycastHit hit;
    private Ray ray;
    public GameObject positionMous;
    private Rigidbody rb;

    public GameObject Bullet;
    public GameObject StartPosition;

    private int coldawn = 1;
    private float time = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        positionMous.transform.position = hit.point;

        Vector3 direction = positionMous.transform.position - transform.position;
        direction.y = 0;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        rb.velocity = transform.forward * 8 + Vector3.up * rb.velocity.y;

        if(time > 0)
        {
            time = time - Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            if (time <= 0)
            {
                GameObject newBullet = PhotonNetwork.Instantiate(Bullet.name, StartPosition.transform.position, Quaternion.identity);
                newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 40, ForceMode.Impulse);
                time = coldawn;
            }
        }
    }
}
