using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class EnemyRoam : MonoBehaviour
{
    AudioSource audioData;

    private const float MinInclusive = -10f;
    private const float MaxInclusive = 10f;
    public Transform[] points;
    //private int destPoint = 0;
    public NavMeshAgent agent;
    private float waitTime;
    public float startWaitTime;


    void Start()
    {
        waitTime = startWaitTime;

        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();

    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        var destPoint = new Vector3(Random.Range(MinInclusive, MaxInclusive), 0, Random.Range(MinInclusive, MaxInclusive));
        // Set the agent to go to the currently selected destination.

        agent.destination = destPoint;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.

    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            audioData = GetComponent<AudioSource>();
            audioData.Play(0);

            if (waitTime <= 0)
            {
                GotoNextPoint();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

}