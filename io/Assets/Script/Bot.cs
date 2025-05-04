using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private PhotonView photonView; 
    public List<GameObject> targetObjects;
    private float minDistance;
    public GameObject target;
    public ParametrBot parametrBot;
    private NavMeshAgent agent;
    public float Score;
    public Ray ray;
    public RaycastHit hit;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        photonView = GetComponent<PhotonView>();
        minDistance = Vector3.Distance(transform.position, targetObjects[0].transform.position);
        target = targetObjects[0];
    }
    void Update()
    {
        if(target != null)
        agent.SetDestination(target.transform.position);

        target = minDistanceObject();

        ray = new Ray(parametrBot.StartPosition.transform.position, transform.forward);
        // Визуализация луча в редакторе
        Debug.DrawRay(parametrBot.StartPosition.transform.position, transform.forward * 55, Color.red);

        if (Physics.Raycast(ray, out hit, 55))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                shot();
            }
        }
    }
    public void shot()
    {
        if (parametrBot.time <= 0 && parametrBot.Boletcount > 0)
        {
            parametrBot.shut();
        }
    }
    private GameObject minDistanceObject()
    {
        if (targetObjects == null || targetObjects.Count == 0)
            return null;

        int j = 0;
        float minDistance = Vector3.Distance(transform.position, targetObjects[0].transform.position);

        for (int i = 1; i < targetObjects.Count; i++)
        {
            if (targetObjects[i] != null)
            {
                float distance = Vector3.Distance(transform.position, targetObjects[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    j = i;
                }
            }
        }

        return targetObjects[j];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return;

        if (other.tag == "Coin")
        {
            other.gameObject.GetComponent<Destroy>().destroy();
            Score++;
            parametrBot.adScore();
        }
        else if (other.tag == "bullet")
        {
            other.gameObject.GetComponent<Destroy>().destroy();
            parametrBot.Boletcount++;
        }
    }
}
