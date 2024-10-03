using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    [CreateAssetMenu(menuName = "GhostRun/Templates/Agent", fileName = "Agent", order = 0)]
    public class AgentTemplate : ScriptableObject
    {
        public int id;

        [Header("기본정보")]
        public string displayName;
        [Multiline] public string description;
        public GameObject prefab;
        public float speed;
        public float timeLimit;
        public Enums.AgentType agentType;
    }
}