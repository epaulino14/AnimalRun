using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : BaseState
{
    public override void Construct()
    {
        motor.anim?.SetTrigger("Fall");
    }
    public override Vector3 ProcessMotion()
    {
        motor.ApplyGravity();

        Vector3 move = Vector3.zero;

        move.x = motor.SnapToLane();
        move.y = motor.verticalVelocity;
        move.z = motor.baseRunSpeed;

        return move;
    }
    public override void Transition()
    {
        if (InputManager.Instance.SwipeLeft)
        {
            motor.ChangeLane(-1);
        }
        if (InputManager.Instance.SwipeRight)
        {
            motor.ChangeLane(1);
        }
            if (motor.isGrounded)
            motor.ChangeState(GetComponent<RunningState>());
    }

}
