using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Bot : Mow
{
    private PhotonView photonView; 
    public List<GameObject> targetObjects;
    public GameObject target;
    public ParametrBot parametrBot;
    private NavMeshAgent agent;
    public Ray ray;
    public RaycastHit hit;
    public TextMeshProUGUI Name;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        photonView = GetComponent<PhotonView>();
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
            if (hit.transform.gameObject.tag == "Player" || hit.transform.gameObject.tag == "Bot")
            {
                shot();
            }
        }
    }
    public void Nikname(string name)
    {
        Nickname = name;
        Name.SetText(name);
    }
    public void adTargetObjects(List<GameObject> targeTObjects)
    {
        targetObjects.Clear();
        targetObjects.AddRange(targeTObjects);
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
        float minDistance = 1000;

        for (int i = 1; i < targetObjects.Count; i++)
        {
            if (targetObjects[i] != null && targetObjects[i].name != gameObject.name)
            {
                float distance = Vector3.Distance(transform.position, targetObjects[i].transform.position);
                if (distance < minDistance)
                {
                    if((targetObjects[i].tag == "Player" || targetObjects[i].tag == "Bot") && parametrBot.Boletcount <= 0)
                    {
                        continue;
                    }
                    else
                    {
                        minDistance = distance;
                        j = i;
                    }
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
