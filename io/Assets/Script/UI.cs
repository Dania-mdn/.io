using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TMP_InputField InputName;

    public void SaveName()
    {
        PlayerPrefs.SetString("PlayerName", InputName.text);
    }
    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
