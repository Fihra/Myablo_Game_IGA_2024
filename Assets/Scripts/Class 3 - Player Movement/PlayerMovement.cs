using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] GameObject testSphere;
    NavMeshAgent agent;

    [SerializeField] float destinationReachedThreshold;
    Vector3 target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDestinationReached();
    }

    void RunClickMovement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.point != Vector3.zero)
            {
                agent.destination = hit.point;
            }
        }
    }

    public void MoveToLocation(Vector3 location)
    {
        target = location;
        agent.destination = location;
    }

    public void CheckDestinationReached()
    {
        float distanceToTarget = MathF.Round(Vector3.Distance(transform.position, target));

        //Debug.Log(distanceToTarget);
        if(distanceToTarget > 1)
        {
            //For Footsteps SFX
            //FMODAudioManager.instance.PlayOneShot(FMODEvents.instance.grassFootsteps, transform.position);
        }
    }
}
