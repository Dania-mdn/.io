using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMow : NetworkBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    public GameObject positionMous;
    private Rigidbody rb;

    public GameObject Bullet;
    public GameObject StartPosition;

    private int coldawn = 1;
    private float time = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        positionMous.transform.position = hit.point;

        if (!isLocalPlayer) return;
        Vector3 direction = positionMous.transform.position - transform.position;
        direction.y = 0;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        rb.velocity = transform.forward * 8;

        if(time > 0)
        {
            time = time - Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            if (time <= 0)
            {
                GameObject newBullet = Instantiate(Bullet, StartPosition.transform.position, Quaternion.identity);
                newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 40, ForceMode.Impulse);
                time = coldawn;
            }
        }
    }
}
