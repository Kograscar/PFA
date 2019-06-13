using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAController : MonoBehaviour
{
    private enum State { GotoRandomPoint, ChasePlayer }

    public float lookRadius = 5f;
    public float maxRadius = 10f;
    public float maxAngle = 30f;
    public Transform randomPoint;

    Transform _target;
    NavMeshAgent _agent;
    State _state = State.GotoRandomPoint;

    void Start()
    {
        _target = PlayerManager.instance.Player.transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        SetState();
        ReactToState();
    }

    void SetState()
    {
        float distance = Vector3.Distance(_target.position, transform.position);

        //On créer un champ de vision face a l'ennemi...
        Vector3 direction = _target.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        bool farDetection = angle < maxAngle && distance < maxRadius;
        bool nearDetection  = distance <= lookRadius;

        if (nearDetection || farDetection)
        {
            _state = State.ChasePlayer;
        }
        else
        {
            _state = State.GotoRandomPoint;
        }
    }

    void ReactToState()
    {
        Debug.Log("Le state est " + _state);

        float distance = Vector3.Distance(_target.position, transform.position);

        switch (_state)
        {
            case State.GotoRandomPoint:
                _agent.SetDestination(randomPoint.position);
                break;

            case State.ChasePlayer:
                _agent.SetDestination(_target.position);

                if (distance <= _agent.stoppingDistance)
                {
                    print("attack");
                }
                break;
        }
    }

    void FaceTaget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, maxRadius);
    }
}
