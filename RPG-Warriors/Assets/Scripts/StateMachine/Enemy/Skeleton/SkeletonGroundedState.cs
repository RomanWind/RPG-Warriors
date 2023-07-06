using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected EnemySkeleton _skeleton;
    protected Transform _player;

    private float _agroDistance = 2f;

    public SkeletonGroundedState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animatorBoolName, EnemySkeleton skeleton) : base(enemyBase, enemyStateMachine, animatorBoolName)
    {
        _skeleton = skeleton;
    }
    public override void Enter()
    {
        base.Enter();

        //temporal
        _player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (_skeleton.IsPlayerDetected() || Vector2.Distance(_skeleton.transform.position, _player.transform.position) < _agroDistance)
            _enemyStateMachine.ChangeState(_skeleton.BattleState);
    }
}
