using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState
{
    public override void Construct()
    {
        motor.verticalVelocity = 0;
    }
    public override Vector3 ProcessMotion()
    {
        Vector3 move = Vector3.zero;
        move.x = motor.SnapToLane();
        move.y = -1.0f;
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
       if (InputManager.Instance.SwipeUp && motor.isGrounded)
        { 
            motor.ChangeState(GetComponent<JumpingState>()); 
        }
        if (!motor.isGrounded)
            motor.ChangeState(GetComponent<FallingState>());
        if (InputManager.Instance.SwipeDown)
            motor.ChangeState(GetComponent<SlidingState>());
    }
}
