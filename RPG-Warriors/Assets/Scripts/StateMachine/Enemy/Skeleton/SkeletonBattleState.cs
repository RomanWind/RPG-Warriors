using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private EnemySkeleton _skeleton;
    private Transform _player;
    private int _moveDirection;

    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animatorBoolName, EnemySkeleton skeleton) : base(enemyBase, enemyStateMachine, animatorBoolName)
    {
        _skeleton = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        
        _player = PlayerManager.instance.GetPlayer().transform;
    }

    public override void Update()
    {
        base.Update();

        if (_skeleton.IsPlayerDetected())
        {
            _stateTimer = _skeleton.GetBattleTime();

            if(_skeleton.IsPlayerDetected().distance < _skeleton.GetAttackDistance() && CanAttack())
                _enemyStateMachine.ChangeState(_skeleton.AttackState);
        }
        else
        {
            if (_stateTimer < 0 || Vector2.Distance(_player.transform.position, _skeleton.transform.position) > _skeleton.GetAgroDistance())
                _enemyStateMachine.ChangeState(_skeleton.IdleState);
        }

        if (_player.position.x > _skeleton.transform.position.x)
            _moveDirection = 1;
        else if (_player.position.x < _skeleton.transform.position.x)
            _moveDirection = -1;

        if (_skeleton.IsPlayerDetected().distance < _skeleton.GetAttackDistance() && _skeleton.IsPlayerDetected())
            return;

        _skeleton.SetVelocity(_skeleton.GetMovementSpeed() * _moveDirection, _enemyRigidbody.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= _skeleton.GetLastTimeAttacked() + _skeleton.GetAttackCooldown())
        {
            _skeleton.SetLastAttackTime(Time.time);
            return true;
        }
        return false;
    }
}
