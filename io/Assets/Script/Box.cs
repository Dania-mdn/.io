using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private bool isDestroy = false;
    private Destroy destroy;

    private float coldawn = 5;

    private void Start()
    {
        destroy = GetComponent<Destroy>();
    }
    private void Update()
    {
        if (!isDestroy) return;

        if(coldawn > 0)
        {
            coldawn = coldawn - 1 * Time.deltaTime;
        }
        else
        {
            destroy.destroy();
        }
    }

    public void SetDestroy()
    {
        isDestroy = true;
    }
}
