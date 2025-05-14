using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametrPlayer : MonoBehaviourPun 
{
    public int LVL = 1;
    private bool isUp = false;
    //mowe
    public int Speed = 20;
    public int UpgradeSpeed = 5;
    //weapon
    public int coldawn = 1;
    public float time = 0;
    public int Boletcount = 3;
    public int DMG = 4;
    public int UpgradeDMG = 2;
    //health
    public int MaxHP = 10;
    public int HP = 10;
    public int UpgradeHP = 3;
    [SerializeField] public Slider HPText; 
    private bool isregen = true;
    //bullet
    public GameObject Bullet;
    public GameObject StartPosition;
    public GameObject endPosition0;
    public GameObject endPosition;

    //skyn
    public GameObject[] Head;
    public GameObject[] Weapon;
    private int j;
    public Animation Anim;

    //linerenderer
    private LineRenderer lineRenderer;

    private void OnEnable()
    {
        EventManage.UpLvl += LvlUp;
        EventManage.MobilUpdate += MobilUpdate;
    }
    private void OnDisable()
    {
        EventManage.UpLvl -= LvlUp;
        EventManage.MobilUpdate -= MobilUpdate;
    }
    private void Start()
    {
        HPText.maxValue = MaxHP;
        HPText.value = HP;
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void MobilUpdate(int i)
    {
        if (i == 1)
            LvlUpSpeed();
        else if (i == 2)
            LvlUpDMG();
        else if (i == 3)
            LvlUpHP();
    }
    public void LvlUp()
    {
        LVL++;
        isUp = true;
    }
    public void LvlUpSpeed()
    {
        Speed = Speed + UpgradeSpeed;
        isUp = false;
        EventManage.DoTakeLvl(1);
        Anim.Play();
    }
    public void LvlUpDMG()
    {
        DMG = DMG + UpgradeDMG;
        isUp = false;
        EventManage.DoTakeLvl(2);
        Anim.Play();
    }
    public void LvlUpHP()
    {
        MaxHP = MaxHP + UpgradeHP;
        isUp = false;
        EventManage.DoTakeLvl(3);
        Anim.Play();
    }
    public void shut()
    {
        EventManage.DoShoot();
        time = coldawn;
        Boletcount--;
        Weapon[j].GetComponent<Animation>().Play();
        Weapon[j].transform.GetChild(0).GetComponent<ParticleSystem>().Play();

        GameObject newBullet = PhotonNetwork.Instantiate(Bullet.name, StartPosition.transform.position, transform.rotation);
        boolet boolet = newBullet.GetComponent<boolet>();
        boolet.numberExplousing = j;
        boolet.damage = DMG;
        boolet.ID = PhotonNetwork.LocalPlayer.ActorNumber;
    }
    private void Update()
    {
        lineRenderer.SetPosition(0, StartPosition.transform.position);
        lineRenderer.SetPosition(1, endPosition0.transform.position);
        lineRenderer.SetPosition(2, endPosition.transform.position);

        if (isregen && HP < MaxHP)
        {
            StartCoroutine(RegenHP());
        }

        if (time > 0)
        {
            time = time - Time.deltaTime;
        }

        if (!isUp) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LvlUpSpeed();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LvlUpDMG();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LvlUpHP();
        }
    }
    private IEnumerator RegenHP()
    {
        isregen = false;

        yield return new WaitForSeconds(5f); // ⏳ Ждём 5 секунд перед началом

        while (HP < MaxHP)
        {
            HP += 1;
            HPText.value = HP;
            yield return new WaitForSeconds(1f); 
        }

        isregen = true;
    }
    public void UpdateDamage(int damage, int id)
    {
        photonView.RPC("TakeDamage", RpcTarget.All, damage); 
        if ((HP -= damage) < 0 && PhotonNetwork.LocalPlayer.ActorNumber == id)
        {
            EventManage.DoDieScore();
        }
    }
    [PunRPC]
    public void TakeDamage(int damage)
    {
        HP -= damage;
        HPText.value = HP; 

        if (HP <= 0)
        {
            Die();
            if (!photonView.IsMine) return;
            EventManage.DoDie();
        }
    }

    void Die()
    {
        PhotonNetwork.Destroy(gameObject);
    }
    [PunRPC]
    void HideObject()
    {
        gameObject.SetActive(false);
    }
    [PunRPC]
    void ActiveObject()
    {
        gameObject.SetActive(true);
    }
    [PunRPC]
    void SetSkin(int head, int weapun)
    {
        for (int i = 0; i < Head.Length; i++)
        {
            if (i != head)
            {
                Head[i].SetActive(false);
            }
            else
            {
                Head[i].SetActive(true);
            }
        }
        for (int i = 0; i < Weapon.Length; i++)
        {
            if (i != weapun)
            {
                Weapon[i].SetActive(false);
            }
            else
            {
                Weapon[i].SetActive(true);
            }
        }
        j = weapun;
    }
}
