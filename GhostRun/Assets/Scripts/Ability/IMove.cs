using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface IMove
    {
        void Move(Transform agentTransform, float speed);
    }
}