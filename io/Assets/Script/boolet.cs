using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boolet : MonoBehaviour
{
    public float speed = 100;
    public int damage;

    private void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1))
        {
            Destroy(gameObject);
        }
    }
}
