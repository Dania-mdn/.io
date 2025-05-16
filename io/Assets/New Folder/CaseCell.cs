using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaseCell : MonoBehaviour
{
    [System.Serializable]
    private class LisrofSprites
    {
        public List<Sprite> Sprites;
    }

    [SerializeField]
    private List<LisrofSprites> sprites;
    [SerializeField]
    int[] chances;
    [SerializeField]
    Sprite[] colors;
    [SerializeField]
    Animation anim;

    public void Setup(int i)
    {
        int index = Randomize();

        if (i == 0)
        {
            GetComponent<Image>().sprite = sprites[index].Sprites[Random.Range(0, sprites[index].Sprites.Count)];
            transform.parent.GetComponent<Image>().sprite = colors[index];
        }
        else
        {
            GetComponent<Image>().sprite = sprites[index].Sprites[Random.Range(0, sprites[index].Sprites.Count)];
            transform.parent.GetComponent<Image>().sprite = colors[2];
        }
    }
    public void Set()
    {
        GetComponent<Image>().sprite = sprites[PlayerPrefs.GetInt("index")].Sprites[Random.Range(0, sprites[PlayerPrefs.GetInt("index")].Sprites.Count)];
        transform.parent.GetComponent<Image>().sprite = colors[PlayerPrefs.GetInt("index")];
        anim.Play();
    }
    private int Randomize()
    {
        int ind = 0;
        for (int i = 0; i < chances.Length; i++)
        {
            int ranf = Random.Range(0, 100);
            if (ranf > chances[i])
                return i;

            ind++;
        }
        return ind;
    }
}
