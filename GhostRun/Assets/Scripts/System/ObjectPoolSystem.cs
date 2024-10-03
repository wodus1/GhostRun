using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class ObjectPoolSystem : MonoBehaviour, ISubSystem
    {
        private Dictionary<Enums.PoolType, List<Stack<GameObject>>> _objectPools = new Dictionary<Enums.PoolType, List<Stack<GameObject>>>();

        private int _poolSize = 10;

        public void Initialize()
        {
            foreach(Enums.PoolType type in Enum.GetValues(typeof(Enums.PoolType)))
            {
                InitializeObjectPool(type);
            }
        }

        private void InitializeObjectPool(Enums.PoolType poolType)
        {
            List<Stack<GameObject>> pools = new List<Stack<GameObject>>();

            switch (poolType)
            {
                case Enums.PoolType.Agent:
                    for (int i = 0; i < GameManager.Instance.agentTemplates.Count; i++)
                    {
                        Stack<GameObject> stack = new Stack<GameObject>();

                        for (int j = 0; j < _poolSize; j++)
                        {
                            var agentData = new AgentData(GameManager.Instance.agentTemplates[i]);
                            GameManager.Instance.AddAgentData(agentData);
                            GameObject obj = Instantiate(agentData.template.prefab,transform);
                            obj.GetComponent<AgentUnit>().Initialize(agentData);
                            
                            obj.SetActive(false);
                            stack.Push(obj);
                        }

                        pools.Add(stack);
                    }
                    break;
            }

            _objectPools[poolType] = pools;
        }

        public void DeInitialize()
        {
            _objectPools.Clear();
        }

        public GameObject GetPoolObject(Enums.PoolType poolType, int idx)
        {
            if (_objectPools.TryGetValue(poolType, out var poolList) && idx >=0)
            {
                if (poolList[idx].Count > 0)
                {
                    GameObject obj = poolList[idx].Pop();
                    obj.SetActive(true);
                    return obj;
                }
                else
                {
                    GameObject newObj;

                    switch (poolType)
                    {
                        case Enums.PoolType.Agent:
                            var agentData = new AgentData(GameManager.Instance.agentTemplates[idx]);
                            GameManager.Instance.AddAgentData(agentData);
                            newObj = Instantiate(agentData.template.prefab, transform);
                            newObj.GetComponent<AgentUnit>().Initialize(agentData);
                            return newObj;
                    }
                }
            }

            return null;
        }

        public void ReturnObjectToPool(Enums.PoolType poolType, GameObject obj, int idx)
        {
            if (_objectPools.TryGetValue(poolType, out List<Stack<GameObject>> pools) && idx >= 0)
            {
                obj.SetActive(false);
                pools[idx].Push(obj);
                _objectPools[poolType] = pools;
            }
        }
    }
}