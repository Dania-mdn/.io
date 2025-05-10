using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGame : MonoBehaviour
{
    public float PositionInTop;
    public GameObject[] array;
    public GameObject baraban;
    private int targetZAngle;
    private int lvl = 1;
    public TextMeshProUGUI lvlText;
    public Slider lvlProgress;

    public int i;
    public float rotationSpeed = 120;

    private float currentZAngle;
    private bool rotating = false;
    public GameObject DiePanel;

    public Slider SliderTimer;
    public TextMeshProUGUI TimerText;
    public Gamemanager gamemanager;
    public GameObject upgrade;
    public TextMeshProUGUI positionInTop;
    public Animation A1;
    public Animation A2;
    public Animation A3;

    private void Start()
    {
        SliderTimer.maxValue = 10;
        SliderTimer.value = 10;
        TimerText.text = 10.ToString();
        baraban.transform.Rotate(0, 0, -180);
        targetZAngle = 180;
        rotating = true;
        lvlProgress.maxValue = 10;
        lvlProgress.value = 0;
        
    }
    private void OnEnable()
    {
        EventManage.Shoot += Shoot;
        EventManage.adBool += adBool;
        EventManage.adScore += adScore;
        EventManage.Die += Die;
        EventManage.TakeLvl += TakeUpgrade;
    }
    private void OnDisable()
    {
        EventManage.Shoot -= Shoot;
        EventManage.adBool -= adBool;
        EventManage.adScore -= adScore;
        EventManage.Die -= Die;
        EventManage.TakeLvl -= TakeUpgrade;
    }
    public void MobilUpdate(int i)
    {
        EventManage.DoMobilUpdate(i);
    }
    private void TakeUpgrade(int i)
    {
        if (i == 1)
            A1.Play();
        else if (i == 2)
            A2.Play();
        else if (i == 3)
            A3.Play();

        Invoke("Upgrade", 2);
    }
    private void Upgrade()
    {
        upgrade.SetActive(false);
    }
    private void Shoot()
    {
        if(targetZAngle < 360)
        {
            targetZAngle = targetZAngle - 58;
            rotating = true;
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
        if (targetZAngle > 0)
        {
            targetZAngle = targetZAngle + 58;
            rotating = true;
        }

        for (int i = array.Length - 1; i >= 0; i--)
        {
            if (array[i].activeSelf == false)
            {
                array[i].SetActive(true);
                break;
            }
        }
    }
    private void adScore()
    {
        if (lvlProgress.value < lvlProgress.maxValue)
        {
            lvlProgress.value++;
        }
        else
        {
            lvlProgress.value = 0;
            lvl++;
            lvlText.text = lvl.ToString();
            EventManage.DoUpLvl();
            upgrade.SetActive(true);
            //lvlProgress.maxValue = ??
        }
    }
    private void Update()
    {
        positionInTop.text = PositionInTop.ToString();

        //if (isDie)
        //{
        //    SliderTimer.value = SliderTimer.value - Time.deltaTime;
        //    TimerText.text = SliderTimer.value.ToString("F0");

        //    if (SliderTimer.value <= 0)
        //    {
        //        gamemanager.LeftRoom();
        //    }
        //}

        if (rotating)
        {
            // Получаем текущий угол Z
            currentZAngle = baraban.transform.eulerAngles.z;

            // Плавно двигаемся к целевому углу
            float newZ = Mathf.MoveTowardsAngle(currentZAngle, targetZAngle, rotationSpeed * Time.deltaTime);

            // Обновляем поворот
            baraban.transform.rotation = Quaternion.Euler(baraban.transform.eulerAngles.x, baraban.transform.eulerAngles.y, newZ);

            // Проверка, дошли ли до нужного угла
            if (Mathf.Approximately(newZ, targetZAngle))
            {
                rotating = false;
            }
        }
    }
    private void Die()
    {
        DiePanel.SetActive(true);
    }
}
