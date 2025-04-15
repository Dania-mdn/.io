using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGame : MonoBehaviour
{
    public GameObject[] array;
    public GameObject baraban;
    private int angle;

    private void Start()
    {
        baraban.transform.Rotate(0, 0, -180);
        angle = 180;
    }
    private void OnEnable()
    {
        EventManage.Shoot += Shoot;
    }
    private void OnDisable()
    {
        EventManage.Shoot -= Shoot;
    }
    private void Shoot()
    {
        if(angle < 360)
        {
            angle = angle - 60;
        }

        for (int i = 0; i < array.Length; i++)
        {
            if(array[i].activeSelf == true)
            {
                array[i].SetActive(false);
                break;
            }
        }
    }
    private void Update()
    {
        if(baraban.transform.eulerAngles.z > angle)
        {
            baraban.transform.Rotate(0, 0, -200 * Time.deltaTime);
        }
    }
}
