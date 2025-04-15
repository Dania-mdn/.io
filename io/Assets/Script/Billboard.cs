using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        // ������������ ������ � ������
        transform.LookAt(Camera.main.transform);

        // ���� �����, ����� ������ �� ��� "����� ������":
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180f, 0);
    }
}
