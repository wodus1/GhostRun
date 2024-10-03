using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Core
{
    public class AgentCreateSystem : MonoBehaviour, ISubSystem
    {
        public List<GameObject> agents { get; private set; } = new List<GameObject>();

        private ObjectPoolSystem _objectPoolSystem;
        private float minDistanceBetweenAgents = 2f;
        private Camera _camera;

        public void Initialize()
        {
            _objectPoolSystem = GameManager.Instance.GetSubSystem<ObjectPoolSystem>();
            _camera = GameManager.Instance.mainCamera;
        }

        public void DeInitialize()
        {

        }

        private void Update()
        {
            for(int i = agents.Count - 1; i >= 0; i--)
            {
                Vector3 agentPos = agents[i].transform.position;
                Vector3 ViewportPos = _camera.WorldToScreenPoint(agentPos);

                if(ViewportPos.y < 0)
                {
                    ReturnAgent(Enums.PoolType.Agent, agents[i], agents[i].GetComponent<AgentUnit>().id);
                }
            }
        }
    
        internal void CreatAgnet(Vector3 pos)
        {
            int agentCount = UnityEngine.Random.Range(2, 5);
            List<Vector3> spawnPositions = GetRandomPositions(pos, agentCount);

            foreach(var spawnPositon in spawnPositions)
            {
                int idx = UnityEngine.Random.Range(0, GameManager.Instance.agentTemplates.Count);

                GameObject obj = _objectPoolSystem.GetPoolObject(Enums.PoolType.Agent, idx);
                obj.transform.position = spawnPositon;

                AgentUnit agentUnit = obj.GetComponent<AgentUnit>();
                agentUnit.Initialize(GameManager.Instance.GetAgentData(agentUnit));
                agents.Add(obj);
            }
        }

        List<Vector3> GetRandomPositions(Vector3 pos, int cnt)
        {
            List<Vector3> positions = new List<Vector3>();

            float AreaMin_z = pos.z;
            float AreaMax_z = pos.z + 20f;
            float AreaMin_x = pos.x - 4f;
            float AreaMax_x = pos.x + 4f;

            int tryCnt = 0;

            while(positions.Count < cnt && tryCnt < 50)
            {
                Vector3 randomPos = new Vector3((float)Math.Round(UnityEngine.Random.Range(AreaMin_x, AreaMax_x)), 0.2f,
                    (float)Math.Round(UnityEngine.Random.Range(AreaMin_z, AreaMax_z)));

                if (IsValidPosition(randomPos, positions))
                {
                    positions.Add(randomPos);
                }

                tryCnt++;
            }

            return positions;
        }

        private bool IsValidPosition(Vector3 newPos, List<Vector3> existingPositions)
        {
            foreach (var pos in existingPositions)
            {
                if (Vector3.Distance(newPos, pos) < minDistanceBetweenAgents)
                {
                    return false;
                }
            }
            return true;
        }

        internal void ReturnAgent(Enums.PoolType pooltype, GameObject obj, int idx)
        {
            if(agents.Contains(obj))
            {
                agents.Remove(obj);
                _objectPoolSystem.ReturnObjectToPool(pooltype, obj, idx);
            }
        }
    }
}