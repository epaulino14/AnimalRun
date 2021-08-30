using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public int currentLane;

    public float distanceInBetweenLanes = 3.0f;
    public float baseRunSpeed = 5.0f;
    public float baseSideWaySpeed = 10.0f;
    public float gravity = 14.0f;
    public float terminalVelocity = 20.0f;

    public CharacterController controller;
    private BaseState state;

    public Animator anim;
    private bool isPaused;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        state = GetComponent<RunningState>();
        state.Construct();

        isPaused = true;
    }

    private void Update()
    {
        if(!isPaused)
            UpdateMotor();
    }

    private void UpdateMotor()
    {
        isGrounded = controller.isGrounded;

        moveVector = state.ProcessMotion();

        state.Transition();

        anim?.SetBool("IsGrounded", isGrounded);
        anim?.SetFloat("speed", Mathf.Abs(moveVector.z));

        controller.Move(moveVector * Time.deltaTime);
    }

    public float SnapToLane()
    {
        float returnValue = 0.0f;

        if(transform.position.x != (currentLane * distanceInBetweenLanes))
        {
            float deltaToDesirePosition = (currentLane * distanceInBetweenLanes) - transform.position.x;
            returnValue = (deltaToDesirePosition > 0) ? 1 : -1;
            returnValue *= baseSideWaySpeed;

            float actualDistance = returnValue * Time.deltaTime;
            if (Mathf.Abs(actualDistance) > Mathf.Abs(deltaToDesirePosition))
                returnValue = deltaToDesirePosition * (1 / Time.deltaTime);

        }
        else
        {
            returnValue = 0;
        }

        return returnValue;

    }

    public void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }

    public void ChangeState(BaseState s)
    {
        state.Destruct();
        state = s;
        state.Construct();

    }

    public void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        if (verticalVelocity < -terminalVelocity)
            verticalVelocity = -terminalVelocity;
    }

    public void PausePlayer()
    {
        isPaused = true;
    }
    public void ResumePlayer()
    {
        isPaused = false;   
    }

    public void RespawnPlayer()
    {
        ChangeState(GetComponent<RespawnState>());
        GameManager.Instance.ChangeCamera(GameCamera.Respawn);
    }

    public void ResetPlayer()
    {
        currentLane = 0;
        transform.position = Vector3.zero;
        anim?.SetTrigger("Idle");
        PausePlayer();
        ChangeState(GetComponent<RunningState>());
       
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string hitLayerName = LayerMask.LayerToName(hit.gameObject.layer);

        if (hitLayerName == "Death")
            ChangeState(GetComponent<DeathState>());

    }

}
