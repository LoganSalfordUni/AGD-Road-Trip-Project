using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected readonly PlayerStateMachine StateMachine;
    protected PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    protected void CalculateMoveDirection()
    {
        var forward = StateMachine.MainCamera.forward;
        Vector3 cameraForward = new(forward.x, 0, forward.z);
        var right = StateMachine.MainCamera.right;
        Vector3 cameraRight = new(right.x, 0, right.z);
        Vector3 moveDirection = cameraForward.normalized * StateMachine.InputReader.moveComposite.y + cameraRight.normalized * StateMachine.InputReader.moveComposite.x;
        StateMachine.velocity.x = moveDirection.x * StateMachine.MovementSpeed;
        StateMachine.velocity.z = moveDirection.z * StateMachine.MovementSpeed;
    }

    protected void FaceMoveDirection()
    {
        Vector3 faceDirection = new(StateMachine.velocity.x, 0f, StateMachine.velocity.z);
        if(faceDirection==Vector3.zero)return;
        StateMachine.transform.rotation=Quaternion.Slerp(StateMachine.transform.rotation,Quaternion.LookRotation(faceDirection),StateMachine.LookRotationDampFactor*Time.deltaTime);
    }

    protected void ApplyGravity()
    {
        if (StateMachine.velocity.y > Physics.gravity.y)
        {
            StateMachine.velocity.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    protected void Move()
    {
        StateMachine.Controller.Move(StateMachine.velocity * Time.deltaTime);
    }
}
