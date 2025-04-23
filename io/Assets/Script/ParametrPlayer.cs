using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametrPlayer : MonoBehaviourPun
{
    public int LVL = 1;
    //health
    public int MaxHP = 10;
    public int HP = 10;
    [SerializeField] public Slider HPText;
    //weapon
    public int coldawn = 1;
    public float time = 0;
    private int maxBolet = 6;
    public int Boletcount = 3;
    public int DMG = 4;
    //mowe
    public int Speed = 20;

    private void Start()
    {
        HPText.maxValue = MaxHP;
        HPText.value = HP;
    }
    public void LvlUpHP()
    {
        LVL++;

    }
    public void LvlUpDMG()
    {
        LVL++;

    }
    public void LvlUpSpeed()
    {
        LVL++;

    }
    private void Update()
    {
        if (time > 0)
        {
            time = time - Time.deltaTime;
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
}
