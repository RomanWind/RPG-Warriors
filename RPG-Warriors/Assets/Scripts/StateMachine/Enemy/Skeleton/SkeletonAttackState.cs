using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private EnemySkeleton _skeleton;

    public SkeletonAttackState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animatorBoolName, EnemySkeleton skeleton) : base(enemyBase, enemyStateMachine, animatorBoolName)
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

        _skeleton.SetLastAttackTime(Time.time);
    }

    public override void Update()
    {
        base.Update();

        _skeleton.SetVelocity(0,0);

        if (_triggerCalled)
            _enemyStateMachine.ChangeState(_skeleton.BattleState);
    }
}
