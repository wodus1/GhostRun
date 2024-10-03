using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        private Dictionary<Type, ISubSystem> _subSystems = new Dictionary<Type, ISubSystem> ();

        [SerializeField] private List<AgentTemplate> _agentTemplates = new List<AgentTemplate>();
        [SerializeField] private List<GameObject> _graoundTemplates = new List<GameObject>();
        [SerializeField] private List<GhostTemplate> _ghostTemplates = new List<GhostTemplate>();
        public HashSet<AgentData> agentDatas { get; private set; } = new HashSet<AgentData>();
        public Camera mainCamera { get; private set; }

        public List<GameObject> groundTemplates => _graoundTemplates;
        public List<AgentTemplate> agentTemplates => _agentTemplates;
        public List<GhostTemplate> ghostTemplates => _ghostTemplates;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if(_instance == null )
                    {
                        GameObject gameObject = new GameObject("GameManager");
                        _instance = gameObject.AddComponent<GameManager>();
                    }
                }
                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (this != _instance)
                {
                    Destroy(this.gameObject);
                }
            }

            var systems = GetComponentsInChildren<ISubSystem>(true);
            foreach(var system in systems)
            {
                _subSystems.Add(system.GetType(), system);
            }

            mainCamera = Camera.main;

            Initialize();

            //테스트용
            GameObject obj = GetSubSystem<ObjectPoolSystem>().GetPoolObject(Enums.PoolType.Agent, 0);
            obj.transform.position = new Vector3(0, 0, -20f);

            AgentUnit agentUnit = obj.GetComponent<AgentUnit>();
            agentUnit.Initialize(GetAgentData(agentUnit));
            GetSubSystem<AgentCreateSystem>().agents.Add(obj);
            agentUnit.ChangeAgentType(Enums.AgentType.Possessed);
            GetSubSystem<CameraSystem>().TargetUnit(agentUnit.gameObject);
            //테스트용
        }

        public void Initialize()
        {
            foreach (var system in _subSystems.Values)
            {
                system.Initialize();
            }
        }

        public void DeInitialize()
        {
            agentDatas.Clear();

            foreach (var system in _subSystems.Values)
            {
                system.DeInitialize();
            }
        }

        internal void AddAgentData(AgentData agentData)
        {
            agentDatas.Add(agentData);
        }

        internal void RemoveAgentData(AgentData agentData)
        {
            agentDatas.Remove(agentData);
        }

        internal AgentData GetAgentData(AgentUnit agent)
        {
            if(agentDatas.TryGetValue(agent.agentData, out AgentData _agentData))
            {
                return _agentData;
            }

            return null;
        }

        internal T GetSubSystem<T>() where T : ISubSystem
        {
            _subSystems.TryGetValue(typeof(T), out var subSystem);
            return (T)subSystem;
        }
    }
}