using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DebugNavMesh : MonoBehaviour
{
    [SerializeField] private bool _velocity;
    [SerializeField] private bool _desieredVelocity;
    [SerializeField] private bool _path;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnDrawGizmos()
    {
        if (_velocity)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + _agent.velocity);
        }

        if (_desieredVelocity)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _agent.desiredVelocity);
        }

        if (_path)
        {
            Gizmos.color = Color.blue;
            var agentPath = _agent.path;

            Vector3 preeveConer = transform.position;

            foreach (var coner in agentPath.corners)
            {
                Gizmos.DrawLine(preeveConer, coner);
                Gizmos.DrawSphere(coner, 0.1f);
                preeveConer = coner;
            }
        }
    }
}
