using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public GameObject Player;
    public float x = 0;
    public float y = 0;
    public float z = 0;
    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(x, y, -z);
    }
    private void Update()
    {
        if (!Player) return;

        transform.position = Player.transform.position + offset;
    }
}
