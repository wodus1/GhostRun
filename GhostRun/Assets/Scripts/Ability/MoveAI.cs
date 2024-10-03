using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    public class MoveAI : IMove
    {
        private Vector3 _forward = new Vector3(0, 0, 1f);
        private NavMeshAgent _navMeshAgent;

        public MoveAI(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
            _navMeshAgent.enabled = true;
        }

        public void Move(Transform agentTransform, float speed)
        {
            if(_navMeshAgent.isOnNavMesh)
            {
                _navMeshAgent.speed = speed;
                _navMeshAgent.SetDestination(agentTransform.position + _forward);
            }
        }
    }
}
