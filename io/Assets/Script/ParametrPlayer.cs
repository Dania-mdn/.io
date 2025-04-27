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
    public int UpgradeSpeed = 20;
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

    //skyn
    public GameObject[] Head;
    public GameObject[] Weapon;

    private void OnEnable()
    {
        EventManage.UpLvl += LvlUp;
    }
    private void OnDisable()
    {
        EventManage.UpLvl -= LvlUp;
    }
    private void Start()
    {
        HPText.maxValue = MaxHP;
        HPText.value = HP;
        if (photonView.IsMine)
            photonView.RPC("SetSkyn", RpcTarget.All, PlayerPrefs.GetInt("heat"), PlayerPrefs.GetInt("weapun"));
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
        EventManage.DoTakeLvl();
    }
    public void LvlUpDMG()
    {
        DMG = DMG + UpgradeDMG;
        isUp = false;
        EventManage.DoTakeLvl();
    }
    public void LvlUpHP()
    {
        MaxHP = MaxHP + UpgradeHP;
        isUp = false;
        EventManage.DoTakeLvl();
    }
    private void Update()
    {
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
            EventManage.DoDie();
        }
    }

    void Die()
    {
        photonView.RPC("HideObject", RpcTarget.All);
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
    void SetSkyn(int head, int weapun)
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
    }
}
