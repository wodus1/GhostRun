using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class GhostSystem : MonoBehaviour, ISubSystem
    {
        private AgentCreateSystem _agentCreateSystem;
        private GhostUnit _ghostUnit;

        public void Initialize()
        {
            _agentCreateSystem = GameManager.Instance.GetSubSystem<AgentCreateSystem>();

            GhostData data = new GhostData(GameManager.Instance.ghostTemplates[0]);
            GameObject ghost = Instantiate(data.template.prefab, transform);
            _ghostUnit = ghost.GetComponent<GhostUnit>();
            _ghostUnit.Initialize(data);
            ghost.SetActive(false);
        }

        public void DeInitialize()
        {

        }

        internal void EnableGhost(AgentUnit agent)
        {
            _agentCreateSystem.ReturnAgent(Enums.PoolType.Agent, agent.gameObject, agent.id);
            _ghostUnit.transform.position = agent.transform.position;
            _ghostUnit.gameObject.SetActive(true);
            _ghostUnit.jumpAbility.Jump();
        }
    }
}