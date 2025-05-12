using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametrBot : MonoBehaviourPun
{
    public int LVL = 1;
    private bool isUp = false;
    //mowe
    public int Speed = 20;
    public int UpgradeSpeed = 20;
    //weapon
    private int score = 0;
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
    //bullet
    public GameObject Bullet;
    public GameObject StartPosition;
    public b b;

    //skyn
    public GameObject[] Head;
    public GameObject[] Weapon;
    public int j;
    public GameObject Not;

    private void Start()
    {
        HPText.maxValue = MaxHP;
        HPText.value = HP;
    }
    public void adScore()
    {
        if(score < 10)
        {
            score++;
        }
        else
        {
            score = 1;
            LvlUp();
        }
                
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
    }
    public void LvlUpDMG()
    {
        DMG = DMG + UpgradeDMG;
        isUp = false;
    }
    public void LvlUpHP()
    {
        MaxHP = MaxHP + UpgradeHP;
        isUp = false;
    }
    public void shut()
    {
        b.Play();
        time = coldawn;
        Boletcount--;
        Weapon[j].GetComponent<Animation>().Play();
        Weapon[j].transform.GetChild(0).GetComponent<ParticleSystem>();

        GameObject newBullet = PhotonNetwork.Instantiate(Bullet.name, StartPosition.transform.position, transform.rotation);
        boolet boolet = newBullet.GetComponent<boolet>();
        boolet.numberExplousing = j;
        boolet.damage = DMG;
    }
    private void Update()
    {
        if (time > 0)
        {
            time = time - Time.deltaTime;
        }

        if (!isUp) return;
    }
    public void UpdateDamage(int damage)
    {
        photonView.RPC("TakeDamage", RpcTarget.All, damage);
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
        }
    }

    void Die()
    {
        PhotonNetwork.Instantiate(Not.name, transform.position, transform.rotation);
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
