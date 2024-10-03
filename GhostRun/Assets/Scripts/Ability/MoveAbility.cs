using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using static Core.Enums;

namespace Core
{
    public class MoveAbility : MonoBehaviour
    {
        private IMove _move;
        private AgentUnit _agent;
        private Enums.AgentType _currentAgentType;
        private GhostSystem _ghostSystem;

        internal void Initialize(AgentUnit agent)
        {
            _agent = agent;
            _currentAgentType = agent.currentAgentType;
            _ghostSystem = GameManager.Instance.GetSubSystem<GhostSystem>();

            CheckAgentType(_currentAgentType);
        }

        private void Update()
        {
            if (_move != null)
            {
                float speed = _agent.normalSpeed;

                if (_currentAgentType == Enums.AgentType.Possessed)
                    speed *= 2f; 

                _move.Move(_agent.transform, speed);

                if(_move is MoveTouch moveTouch)
                {
                    if(!moveTouch.isTouch)
                    {
                        _ghostSystem.EnableGhost(_agent);
                    }
                }
            }
        }

        internal void CheckAgentType(Enums.AgentType agentType)
        {

            if (agentType == Enums.AgentType.Human)
            {
                _move = new MoveAI(_agent.navMeshAgent);
            }
            else if (agentType == Enums.AgentType.Possessed)
            {
                _move = new MoveTouch();
            }
        }
    }
}