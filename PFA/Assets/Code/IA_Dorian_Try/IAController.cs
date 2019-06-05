using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAController : MonoBehaviour
{
    public float lookRadius = 5f;
    public Transform randomPoint;

    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        target = PlayerManager.instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        float distanceRandomPoint = Vector3.Distance(randomPoint.position, transform.position);

        //On créer un champ de vision face a l'ennemi...
        Vector3 direction = target.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if (Vector3.Distance(target.position, this.transform.position) < 10 && angle < 30)
        {
            print("CA MARCHE");
            agent.SetDestination(target.position);
        }

        //On dit a l'ennemi d'aller au randomPoint...
        if (distance > lookRadius && Vector3.Distance(target.position, this.transform.position) > 10 && angle > 30)
        {
            agent.SetDestination(randomPoint.position);
        }

        //On dit a l'ennemi de traquer le joueur et de l'attaquer...
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                print("attack");
            }
        }

    }

    void FaceTaget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, lookRadius);
    }
}
