using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    private EnemySkeleton _skeleton;

    public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animatorBoolName, EnemySkeleton skeleton) : base(enemyBase, enemyStateMachine, animatorBoolName)
    {
        _skeleton = skeleton;
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

        _skeleton.SetVelocity(_skeleton.GetMovementSpeed() * _skeleton.FacingDirection, _skeleton.EnemyRigidbody.velocity.y);

        if (_skeleton.IsWallDetected() || !_skeleton.IsGroundDetected())
        {
            _skeleton.Flip();
            _enemyStateMachine.ChangeState(_skeleton.SkeletonIdleSt);
        }    
    }
}
