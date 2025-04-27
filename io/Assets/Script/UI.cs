using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public float sensitivity = 15.0f;
    public Transform PlayerModel;
    float xAxis;

    public TMP_InputField InputName;
    public GameObject Camera;
    private Vector3 StartPosition;
    private Vector3 ShopPosition;
    public GameObject[] Head;
    public GameObject[] Weapon;

    private bool isRedyToTurn = false; 
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = PlayerModel.rotation;
        StartPosition = Camera.transform.position;
        ShopPosition = new Vector3(70.8f, 73.3f, -77.4f);
    }
    void Update()
    {
        if (!isRedyToTurn) return;

        if (Input.GetKey(KeyCode.Mouse0))
            xAxis += Input.GetAxisRaw("Mouse X") * sensitivity;
        PlayerModel.transform.eulerAngles = new Vector3(PlayerModel.transform.eulerAngles.x, -xAxis, PlayerModel.transform.eulerAngles.z);
    }
    public void SetTurn(bool turn)
    {
        isRedyToTurn = turn;

        if(turn == false)
        {
            StartCoroutine(RotateBackCoroutine());
        }
    }
    private System.Collections.IEnumerator RotateBackCoroutine()
    {
        Quaternion currentRotation = PlayerModel.rotation;
        float elapsed = 0f;
        float duration = 1f; // за сколько секунд вернуть

        while (elapsed < duration)
        {
            PlayerModel.rotation = Quaternion.Slerp(currentRotation, initialRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = initialRotation; // на всякий случай точно устанавливаем финальное положение
    }
    public void SetShop(bool isShop)
    {
        if (isShop)
        {
            StartCoroutine(MoveOverTime(Camera.transform, Camera.transform.position, ShopPosition, 1f));
        }
        else
        {
            StartCoroutine(MoveOverTime(Camera.transform, Camera.transform.position, StartPosition, 1f));
        }
    }
    public IEnumerator MoveOverTime(Transform obj, Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            obj.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.position = endPos;
    }
    public void SaveName()
    {
        PlayerPrefs.SetString("PlayerName", InputName.text);
    }
    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }
    public void SetHead(int number)
    {
        PlayerPrefs.SetInt("heat", number);
        for (int i = 0; i < Head.Length; i++)
        {
            if(i != number)
            {
                Head[i].SetActive(false);
            }
            else
            {
                Head[i].SetActive(true);
            }
        }
    }
    public void SetWeapun(int number)
    {
        PlayerPrefs.SetInt("weapun", number);
        for (int i = 0; i < Weapon.Length; i++)
        {
            if(i != number)
            {
                Weapon[i].SetActive(false);
            }
            else
            {
                Weapon[i].SetActive(true);
            }
        }
    }
}
