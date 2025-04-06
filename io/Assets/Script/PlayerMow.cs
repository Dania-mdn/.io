using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMow : MonoBehaviour
{
    public PhotonView photonView;
    public Vector2Int GamePosition;

    private RaycastHit hit;
    private Ray ray;
    public GameObject positionMous;
    public int Score;

    public GameObject Bullet;
    public GameObject StartPosition;

    private int coldawn = 1;
    private float time = 0;
    public TextMeshProUGUI Name;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        Name.SetText(photonView.Owner.NickName);
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        GamePosition = new Vector2Int((int)transform.position.x, (int)transform.position.z);

        Name.text = PhotonNetwork.NickName.ToString();

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        positionMous.transform.position = hit.point;

        Vector3 direction = positionMous.transform.position - transform.position;
        direction.y = 0;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        transform.Translate(transform.forward * Time.deltaTime * 20, Space.World);

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
