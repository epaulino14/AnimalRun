using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingState : BaseState
{
    public float slideDuration = 1.0f;

    //collider logic

    private Vector3 initialCenter;
    private float initialSize;
    private float slideStart;

    public override void Construct()
    {
        motor.anim?.SetTrigger("slide");
        slideStart = Time.time;

        initialSize = motor.controller.height;
        initialCenter = motor.controller.center;

        motor.controller.height = initialSize * 0.5f;
        motor.controller.center = initialCenter * 0.5f;

    }

    public override void Destruct()
    {
        
        motor.controller.height = initialSize;
        motor.controller.center = initialCenter;
        motor.anim?.SetTrigger("Running");

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

        if (!motor.isGrounded)
            motor.ChangeState(GetComponent<FallingState>());
        if (InputManager.Instance.SwipeUp)
            motor.ChangeState(GetComponent<JumpingState>());
        if (Time.time - slideStart > slideDuration)
            motor.ChangeState(GetComponent<RunningState>());


    }

    public override Vector3 ProcessMotion()
    {
        Vector3 move = Vector3.zero;
        move.x = motor.SnapToLane();
        move.y = -1.0f;
        move.z = motor.baseRunSpeed;

        return move;
    }
}
