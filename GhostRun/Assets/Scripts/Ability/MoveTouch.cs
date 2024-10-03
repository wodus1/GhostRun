using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    public class MoveTouch : IMove
    {
        private float _lastTouchPositionX;
        private Vector3 _forward = new Vector3(0, 0, 1f);
        
        public bool isTouch = true;

        public void Move(Transform agentTransform, float speed)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                _lastTouchPositionX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                float deltaX = Input.mousePosition.x - _lastTouchPositionX;
                Vector3 moveDirection = _forward + new Vector3(deltaX, 0, 0);
                agentTransform.Translate(moveDirection * speed * Time.deltaTime);
                _lastTouchPositionX = Input.mousePosition.x;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                isTouch = false;
            }
            else
            {
                agentTransform.Translate(_forward * speed * Time.deltaTime);
            }
#endif

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _lastTouchPositionX = touch.position.x;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    float deltaX = touch.position.x - _lastTouchPositionX;
                    Vector3 moveDirection = _forward + new Vector3(deltaX, 0, 0);
                    agentTransform.Translate(moveDirection * speed * Time.deltaTime);
                    _lastTouchPositionX = touch.position.x;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    isTouch = false;
                }
            }
            else
            {
                agentTransform.Translate(_forward * speed * Time.deltaTime);
            }
        }
    }
}
