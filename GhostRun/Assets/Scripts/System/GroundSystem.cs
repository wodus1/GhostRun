using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

namespace Core
{
    public class GroundSystem : MonoBehaviour, ISubSystem
    {
        private AgentCreateSystem _agentCreateSystem;
        private Camera _camera;
        [SerializeField]private NavMeshSurface _navMeshSurface;

        private List<GameObject> _roads = new List<GameObject>();
        private Vector3 _roadBounds;
        private int _currentIndex;

        public void Initialize()
        {
            _agentCreateSystem = GameManager.Instance.GetSubSystem<AgentCreateSystem>();
            _camera = GameManager.Instance.mainCamera;

            for (int i = 0; i < 3; i++)
            {
                _roads.Add(Instantiate(GameManager.Instance.groundTemplates[0], transform));
            }

            _roadBounds = _roads[0].gameObject.GetComponent<Renderer>().bounds.size;

            for (int i = 0; i < 3; i++)
            {
                _roads[i].transform.position = new Vector3(0, 0, _roadBounds.z * i);
            }

            _navMeshSurface.BuildNavMesh();

            _currentIndex = 0;

            _agentCreateSystem.CreatAgnet(_roads[_currentIndex].transform.position);
            _agentCreateSystem.CreatAgnet(_roads[_currentIndex].transform.position + new Vector3(0, 0, 40));
        }
        

        public void DeInitialize()
        {
            _roads.Clear();
        }

        void Update()
        {
            foreach(var road in _roads)
            {
                CheckAndRepositionRoad(road);
            }
        }

        private void CheckAndRepositionRoad(GameObject road)
        {
            Vector3 topOfRoad = road.transform.position + new Vector3(0, 0, _roadBounds.z / 2);
            Vector3 roadViewportPos = _camera.WorldToViewportPoint(topOfRoad);

            if (roadViewportPos.y < 0)
            {
                DisableAndRepositionRoad(road);
            }
        }

        private void DisableAndRepositionRoad(GameObject road)
        {
            Vector3 newPosition = road.transform.position;
            newPosition.z += _roadBounds.z * 3;

            road.transform.position = newPosition;

            _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);

            _currentIndex = (_currentIndex + 1) % 3;
            _agentCreateSystem.CreatAgnet(_roads[_currentIndex].transform.position);
            _agentCreateSystem.CreatAgnet(_roads[_currentIndex].transform.position + new Vector3(0, 0, 40));
        }
    }
}