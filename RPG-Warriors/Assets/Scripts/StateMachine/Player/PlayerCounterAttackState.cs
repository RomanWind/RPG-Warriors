using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _stateTimer = _player.CounterAttackDuration;
        _player.EntityAnimator.SetBool("SuccessfullCounterAttack", false);
    }

    public override void Update()
    {
        base.Update();

        _player.SetZeroVelocity();
        TryStunEnemies();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void TryStunEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_player.AttackCheck.position, _player.AttackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if(hit.GetComponent<Enemy>().CanBeStunned())
                {
                    _stateTimer = 10f;
                    _player.EntityAnimator.SetBool("SuccessfullCounterAttack", true);

                }
            }
        }

        if (_stateTimer < 0 || _triggerCalled)
            _stateMachine.ChangeState(_player.IdleState);
    }
}
