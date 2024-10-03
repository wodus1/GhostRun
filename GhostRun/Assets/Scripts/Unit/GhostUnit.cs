using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    [RequireComponent(typeof(JumpAbility))]
    [RequireComponent(typeof(Rigidbody))]
    public class GhostUnit : MonoBehaviour
    {
        public GhostData ghostData { get; private set; }
        public JumpAbility jumpAbility { get; private set; }
        public Rigidbody rigid { get; private set; }
        public CameraSystem cameraStstem { get; private set; }

        public int id { get; private set; }
        public float normalPower { get; private set; }

        private void Awake()
        {
            jumpAbility = GetComponent<JumpAbility>();
            rigid = GetComponent<Rigidbody>();
        }

        internal void Initialize(GhostData ghostData)
        {
            this.ghostData = ghostData;
            id = ghostData.id;
            normalPower = ghostData.power;
            jumpAbility.Initialize(this);
            cameraStstem = GameManager.Instance.GetSubSystem<CameraSystem>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("agent"))
            {
                AgentUnit agentUnit = collision.gameObject.GetComponent<AgentUnit>();
                agentUnit.navMeshAgent.enabled = false;
                agentUnit.ChangeAgentType(Enums.AgentType.Possessed);
                cameraStstem.TargetUnit(agentUnit.gameObject);
                gameObject.SetActive(false);
            }
            else
            {
                Die();
            }
        }

        private void Die()
        {
        }
    }
}
