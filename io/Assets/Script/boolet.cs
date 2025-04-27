using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class boolet : MonoBehaviour
{
    public float speed = 100;
    public int damage;
    public GameObject boom;

    private void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 6))
        {
            if (hit.transform.CompareTag("Player"))
            {
                ParametrPlayer pv = hit.transform.gameObject.GetComponent<ParametrPlayer>();

                if (pv != null)
                {
                    pv.UpdateDamage(damage);
                }
            }
            else if (hit.transform.CompareTag("box"))
            {
                hit.transform.gameObject.GetComponent<Animation>().Play();
            }

            PhotonNetwork.Instantiate(boom.name, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
