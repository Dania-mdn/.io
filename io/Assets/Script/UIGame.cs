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
        EventManage.adBool += adBool;
    }
    private void OnDisable()
    {
        EventManage.Shoot -= Shoot;
        EventManage.adBool -= adBool;
    }
    private void Shoot()
    {
        if(angle < 360)
        {
            angle = angle - 58;
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
    private void adBool()
    {
        if (angle > 0)
        {
            angle = angle + 58;
        }

        for (int i = array.Length - 1; i > 0; i--)
        {
            if (array[i].activeSelf == false)
            {
                array[i].SetActive(true);
                break;
            }
        }
    }
    private void Update()
    {
        if(Mathf.Round(baraban.transform.eulerAngles.z) > angle)
        {
            baraban.transform.Rotate(0, 0, -100 * Time.deltaTime);
        }
        else if (Mathf.Round(baraban.transform.eulerAngles.z) < angle)
        {
            baraban.transform.Rotate(0, 0, 100 * Time.deltaTime);
        }
    }
}
