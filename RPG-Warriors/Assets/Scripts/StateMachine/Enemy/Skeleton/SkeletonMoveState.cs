using UnityEngine;

public class SkeletonMoveState : SkeletonGroundedState
{
    public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animatorBoolName, EnemySkeleton skeleton) : base(enemyBase, enemyStateMachine, animatorBoolName, skeleton)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        _skeleton.SetVelocity(_skeleton.GetMovementSpeed() * _skeleton.FacingDirection, _enemyRigidbody.velocity.y);

        if (_skeleton.IsWallDetected() || !_skeleton.IsGroundDetected())
        {
            _skeleton.Flip();
            _enemyStateMachine.ChangeState(_skeleton.IdleState);
        }    
    }
}
