using System;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int _moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private const float AnimationDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        StateMachine.velocity.y = Physics.gravity.y;
        StateMachine.Animator.CrossFadeInFixedTime(_moveBlendTreeHash,CrossFadeDuration);
    }

    public override void Tick()
    {
        //if (!_stateMachine.Controller.isGrounded)
        //{
        //    _stateMachine.SwitchState(new PlayerFallState(_stateMachine));
        //}
        CalculateMoveDirection();
        FaceMoveDirection();
        Move();
        StateMachine.Animator.SetFloat(_moveSpeedHash,StateMachine.InputReader.moveComposite.sqrMagnitude>0f?1f:0f,AnimationDampTime,Time.deltaTime);
        
    }

    public override void Exit()
    {
        throw new NotImplementedException();
    }
}
