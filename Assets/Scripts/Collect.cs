using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Collect : StateMachineBehaviour
{
    private NavMeshAgent _agent;
    private GameObject _target;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent = animator.GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag ("Collectible");
        if (_target != null)
        {
            _agent.SetDestination(_target.transform.position);
            Debug.Log("Иду к предмету: " + _target.name);
        }

        else
        {
            Debug.Log("Предмет не найден.");
            _agent.isStopped = true;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_target != null)
        {
            Destroy(_target);
            Debug.Log($"Предмет собран: {_target}");

            _target = null;

            animator.SetTrigger("Idle");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.isStopped = false;
    }
}