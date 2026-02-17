using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Search : StateMachineBehaviour
{
    public float _searchRadius = 5f;
    private NavMeshAgent _agent;
    private GameObject _target;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.isStopped = false;
        MoveToRandomLocation();
    }

    private void MoveToRandomLocation()
    {
        Vector3 _randomDirection = Random.insideUnitCircle * _searchRadius;
        _randomDirection += _agent.transform.position;
        NavMeshHit _hit;

        if (NavMesh.SamplePosition(_randomDirection, out _hit, _searchRadius, NavMesh.AllAreas))
        {
            _agent.SetDestination(_hit.position);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider[] _hitColliders = Physics.OverlapSphere(_agent.transform.position, 5f);
        foreach (var _hitCollider in _hitColliders)
        {
            if (_hitCollider.CompareTag("Collectible"))
            {
                _target = _hitCollider.gameObject;
                _agent.SetDestination(_target.transform.position);
                break;
            }
        }

        if (_target != null)
        {
            animator.SetTrigger("Collect");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.isStopped = false;
    }
}