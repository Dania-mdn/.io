using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMow : Mow
{
    public ParametrPlayer parametrPlayer;
    private PhotonView photonView;
    public Gamemanager gamemanager;
    public Vector2Int GamePosition;

    private LayerMask mask;
    private RaycastHit hit;
    private Ray ray;
    public GameObject positionMous;
    public int NewScore;

    public TextMeshProUGUI Name;

    public List<PlayerMow> Players = new List<PlayerMow>();
    private CharacterController controller;

    //временная хуйня
    private bool go = true;

    private void Start()
    {
        mask = LayerMask.GetMask("ground");
        controller = GetComponent<CharacterController>();
        photonView = GetComponent<PhotonView>();

        Name.SetText(photonView.Owner.NickName);

        OwnerID = photonView.Owner.ActorNumber;

        //временная хуйня
        StartCoroutine(FindPlayerMowDelayed());
    }
    //временная хуйня
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

        Vector3 inputPosition;

        if (Input.GetMouseButton(0))
        {
            shot();
        }

        if (Input.touchCount > 0)
        {
            inputPosition = Input.GetTouch(0).position;
        }
        else
        {
            inputPosition = Input.mousePosition;
        }
        inputPosition = Input.mousePosition;

        ray = Camera.main.ScreenPointToRay(inputPosition);
        Physics.Raycast(ray, out hit, Mathf.Infinity, mask);

        positionMous.transform.position = hit.point;

        Vector3 direction = positionMous.transform.position - transform.position;
        direction.y = 0;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;

        Vector3 move = transform.forward * parametrPlayer.Speed * Time.deltaTime;
        controller.Move(move);
    }
    public void shot()
    {
        if (parametrPlayer.time <= 0 && parametrPlayer.Boletcount > 0)
        {
            parametrPlayer.shut();
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
            if (parametrPlayer.Boletcount < 6)
            {
                other.gameObject.GetComponent<Destroy>().destroy();
                parametrPlayer.Boletcount++;
                EventManage.DoadBool();
            }
        }
    }
}
