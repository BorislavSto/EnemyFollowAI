using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{

    public NavMeshAgent enemy;
    public Transform Player;
    public float timer = 0.5f;
    public Transform LastPosition;


    void Start()
    {
        Player = GameObject.Find("FirstPersonController").transform;
        LastPosition = GameObject.Find("LastPosition").transform;

    }

    void Update()
    {

        if (infront() && inLineOfSight())
        {
           
                NavMesh.SamplePosition(Player.position, out NavMeshHit hit, 3, int.MaxValue);
                enemy.SetDestination(hit.position);
                Debug.Log("Player found");

            GetComponent<NavMeshAgent>().speed = 4f;
        }
        if (infront() && !inLineOfSight())
        {
            
            timer = 0.5f;
            GetComponent<NavMeshAgent>().speed = 2f;
        }
    }

    bool infront()
    {
        Vector3 directionOfPlayer = transform.position - Player.position;
        float angle = Vector3.Angle(transform.forward, directionOfPlayer);

        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            return true;
        }
        return false;
    }

    bool inLineOfSight()
    {
        RaycastHit _hit;
        Vector3 directionofPlayer = Player.position - transform.position;
        Debug.DrawLine(Player.position, transform.position);
        if (Physics.Raycast(transform.position, directionofPlayer, out _hit, 1000f))
        {
            Debug.DrawRay(transform.position, directionofPlayer, Color.red);
            if (_hit.transform.name == "FirstPersonController")
            {
                return true;
            }
        }
        return false;
    }

}
