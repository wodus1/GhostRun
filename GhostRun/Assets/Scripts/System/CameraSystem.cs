using Cinemachine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour, ISubSystem
{
    private Vector3 _offset = new Vector3(5,8,-5);
    private  GameObject _target;
    private Transform _cameraTransform;

    public void Initialize()
    {
        _cameraTransform = GameManager.Instance.mainCamera.transform;
    }

    public void DeInitialize()
    {

    }
    void LateUpdate()
    {
        if (_target != null)
        {
            _cameraTransform.position = new Vector3(0, _target.transform.position.y, _target.transform.position.z) + _offset;

            transform.LookAt(_target.transform);
        }
    }

    internal void TargetUnit(GameObject unit)
    {
        _target = unit;
    }
}
