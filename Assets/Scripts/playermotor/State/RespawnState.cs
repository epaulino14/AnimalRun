using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnState : BaseState
{
    [SerializeField] private float verticalDistance = 25.0f;
    [SerializeField] private float immunityTime = 1f;

    private float startTime;
    public override void Construct()
    {
        startTime = Time.time;

        motor.controller.enabled = false;
        motor.transform.position = new Vector3(0, verticalDistance, motor.transform.position.z);
        motor.controller.enabled = true;

        motor.verticalVelocity = 0.0f;
        motor.currentLane = 0;
        motor.anim?.SetTrigger("Respawn");

        
    }
    public override void Destruct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Game);
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
        if (motor.isGrounded && (Time.time - startTime) > immunityTime)
            motor.ChangeState(GetComponent<RunningState>());
        if (InputManager.Instance.SwipeLeft)
        {
            motor.ChangeLane(-1);
        }
        if (InputManager.Instance.SwipeRight)
        {
            motor.ChangeLane(1);
        }
    }
}
