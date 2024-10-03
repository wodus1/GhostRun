using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    [RequireComponent(typeof(MoveAbility))]

    public class AgentUnit : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public AgentData agentData { get; private set; }
        public MoveAbility moveAbility { get; private set; }
        public Enums.AgentType currentAgentType { get; private set; }
        public int id { get; private set; }
        public float normalSpeed { get; private set; }
        public float normalTimeLimit { get; private set; }

        public NavMeshAgent navMeshAgent => _navMeshAgent;

        private void Awake()
        {
            moveAbility = GetComponent<MoveAbility>();
        }

        internal void Initialize(AgentData agentData)
        {
            this.agentData = agentData;
            id = agentData.id;
            normalSpeed = agentData.normalSpeed;
            normalTimeLimit = agentData.normalTimeLimit;
            currentAgentType = agentData.agentType;
            moveAbility.Initialize(this);
        }

        internal void ChangeAgentType(Enums.AgentType agentType)
        {
            currentAgentType = agentType;
            moveAbility.CheckAgentType(currentAgentType);
        }
    }
}