using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametrPlayer : MonoBehaviour
{
    public int LVL = 1;
    //health
    public int HP = 10;
    //weapon
    public int coldawn = 1;
    public float time = 0;
    private int maxBolet = 6;
    public int Boletcount = 3;
    public int DMG = 4;
    //mowe
    public int Speed = 20;

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
}
