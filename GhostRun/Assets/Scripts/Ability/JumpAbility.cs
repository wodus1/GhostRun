using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : MonoBehaviour
{
    private GhostUnit _ghost;
    private Rigidbody _rigid;
    private float _jumpForce = 5f;

    internal void Initialize(GhostUnit ghost)
    {
        _ghost = ghost;
        _rigid = ghost.rigid;
    }

    internal void Jump()
    {
        _ghost.cameraStstem.TargetUnit(_ghost.gameObject);

        if (_rigid != null)
        {
            _rigid.angularVelocity = Vector3.zero;
            Vector3 jumpVelocity = new Vector3(0, _jumpForce, _ghost.normalPower);
            _rigid.velocity = Vector3.zero;
            _rigid.AddForce(jumpVelocity, ForceMode.VelocityChange);
        }
    }
}
