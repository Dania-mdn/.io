using System.Collections.Generic;
using UnityEngine;

public class CaseScroll : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject line;
    float speed;
    bool isScroll;
    bool isTu = false;
    List<CaseCell> cels = new List<CaseCell>();

    private void Start()
    {
        Invoke("Scroll", 1);
        Invoke("Scroll", 6);
    }
    public void Scroll()
    {
        if (line.activeSelf == false)
            line.SetActive(true);

        if (isScroll) return;

        GetComponent<RectTransform>().localPosition = new Vector3(1000, 300);

        speed = 4;
        isScroll = true;

        if (cels.Count == 0)
            for (int i = 0; i < 50; i++)
            {
                cels.Add(Instantiate(prefab, transform).GetComponentInChildren<CaseCell>());
            }

        for (int i = 0; i < cels.Count; i++)
        {
            if (i == 34)
            {
                if(isTu)
                    cels[i].Setup(1);
                else
                    cels[i].Setup(0);
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

        transform.Translate(Vector3.left * speed * Time.deltaTime * 1500);

        // Плавное замедление
        speed -= Time.deltaTime;
        if (speed <= 0)
        {
            speed = 0;
            isScroll = false;
            isTu = true;
        }
    }
}
