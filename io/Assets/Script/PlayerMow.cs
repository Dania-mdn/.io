using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMow : MonoBehaviourPun
{
    public int OwnerID;
    public ParametrPlayer parametrPlayer;
    public PhotonView photonView;
    public Gamemanager gamemanager;
    public Vector2Int GamePosition;

    private LayerMask mask;
    private RaycastHit hit;
    private Ray ray;
    public GameObject positionMous;
    public float Score;
    public int NewScore;

    public GameObject Bullet;
    public GameObject StartPosition;

    public TextMeshProUGUI Name;

    public List<PlayerMow> Players = new List<PlayerMow>();
    private CharacterController controller;

    //��������� �����
    private bool go = true;

    private void Start()
    {
        mask = LayerMask.GetMask("ground");
        controller = GetComponent<CharacterController>();
        photonView = GetComponent<PhotonView>();

        Name.SetText(photonView.Owner.NickName);

        OwnerID = photonView.Owner.ActorNumber;

        //��������� �����
        StartCoroutine(FindPlayerMowDelayed());
    }
    //��������� �����
    private IEnumerator FindPlayerMowDelayed()
    {
        yield return new WaitForSeconds(1);
        go = false;
    }
    ////////////
    private void Update()
    {
        if (!photonView.IsMine) return;

        if (!go)
        {
            gamemanager.SendScore(Score, OwnerID);
        }
        GamePosition = new Vector2Int((int)transform.position.x, (int)transform.position.z);

        Name.text = PhotonNetwork.NickName.ToString();

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, Mathf.Infinity, mask);

        positionMous.transform.position = hit.point;

        Vector3 direction = positionMous.transform.position - transform.position;
        direction.y = 0;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        Vector3 move = transform.forward * parametrPlayer.Speed * Time.deltaTime;
        controller.Move(move);


        if (Input.GetMouseButton(0))
        {
            if (parametrPlayer.time <= 0 && parametrPlayer.Boletcount > 0)
            {
                GameObject newBullet = PhotonNetwork.Instantiate(Bullet.name, StartPosition.transform.position, transform.rotation);
                boolet boolet = newBullet.GetComponent<boolet>();
                boolet.damage = parametrPlayer.DMG;
                parametrPlayer.time = parametrPlayer.coldawn;
                parametrPlayer.Boletcount--;
                EventManage.DoShoot();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return;

        if (other.tag == "Coin")
        {
            other.gameObject.GetComponent<Destroy>().destroy();
            Score++;
            EventManage.DoadScore();
        }
        else if (other.tag == "bullet")
        {
            other.gameObject.GetComponent<Destroy>().destroy();
            parametrPlayer.Boletcount++;
            EventManage.DoadBool();
        }
    }
}
