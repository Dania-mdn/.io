using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaseScroll : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject line;
    [SerializeField]
    GameObject Plane;
    [SerializeField]
    CaseCell WinCase;
    float speed;
    bool isScroll;
    bool isTu = false;
    List<CaseCell> cels = new List<CaseCell>();
    public ParticleSystem particleSystem;

    private void Start()
    {
        Invoke("Scroll", 1);
        Invoke("Scroll", 6);
    }
    public void Scroll()
    {
        if (line.activeSelf == false)
        {
            line.SetActive(true);
            Plane.SetActive(true);
        }

        if (isScroll) return;

        GetComponent<RectTransform>().localPosition = new Vector3(1000, 300);
        WinCase.transform.parent.gameObject.SetActive(false);

        speed = 4;
        isScroll = true;

        if (cels.Count == 0)
            for (int i = 0; i < 50; i++)
            {
                cels.Add(Instantiate(prefab, transform).GetComponentInChildren<CaseCell>());
            }

        for (int i = 0; i < cels.Count; i++)
        {
            if (i == 27)
            {
                if (isTu)
                {
                    cels[i].Setup(1);
                }
                else
                {
                    cels[i].Setup(0);
                }
            }
            else
            {
                cels[i].Setup(0);
            }
        }
    }
    private void Update()
    {
        if (!isScroll) return;

        // Получаем RectTransform
        RectTransform rt = GetComponent<RectTransform>();

        // Смещаем позицию по X
        Vector3 pos = rt.anchoredPosition;
        pos.x -= speed * Time.deltaTime * 1500;
        rt.anchoredPosition = pos;

        // Плавное замедление
        speed -= Time.deltaTime;
        if (speed <= 0)
        {
            speed = 0;
            isScroll = false;
            isTu = true;

            WinCase.GetComponent<Image>().sprite = cels[27].GetComponent<Image>().sprite;
            WinCase.transform.parent.GetComponent<Image>().sprite = cels[27].transform.parent.GetComponent<Image>().sprite;
            WinCase.transform.parent.gameObject.SetActive(true);
            particleSystem.Play();
            PlayerPrefs.DeleteKey("index");
        }
    }
    private void OnParticl()
    {
        particleSystem.Play();
    }
}
