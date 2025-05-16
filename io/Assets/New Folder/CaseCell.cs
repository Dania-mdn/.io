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
    Color[] colors;
    [SerializeField]
    Animation anim;
    public void Setup(int i)
    {
        var index = Randomize();
        if (i == 0)
        {
            GetComponent<Image>().sprite = sprites[index].Sprites[Random.Range(0, sprites[index].Sprites.Count)];
            transform.parent.GetComponent<Image>().color = colors[index];
            Debug.Log(1);
        }
        else
        {
            GetComponent<Image>().sprite = sprites[index].Sprites[Random.Range(0, sprites[index].Sprites.Count)];
            transform.parent.GetComponent<Image>().color = colors[2];
            if (anim != null)
            anim.Play();
        }
    }
    public void Set(int i)
    {
        GetComponent<Image>().sprite = sprites[1].Sprites[1];
        transform.parent.GetComponent<Image>().color = colors[1];
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
