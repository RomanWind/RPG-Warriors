using UnityEngine;

public class SkeletonIdleState : SkeletonGroundedState
{
    public SkeletonIdleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animatorBoolName, EnemySkeleton skeleton) : base(enemyBase, enemyStateMachine, animatorBoolName, skeleton)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _stateTimer = _skeleton.GetIdleTime();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (_stateTimer < 0)
            _enemyStateMachine.ChangeState(_skeleton.MoveState);
    }
}
