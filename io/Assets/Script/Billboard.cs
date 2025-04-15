using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        // Поворачиваем объект к камере
        transform.LookAt(Camera.main.transform);

        // Если нужно, чтобы объект не был "задом наперёд":
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180f, 0);
    }
}
