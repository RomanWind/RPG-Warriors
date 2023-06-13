using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    private float _wallJumpDistance = 5f;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _stateTimer = 0.4f;
        _player.SetVelocity(_wallJumpDistance * -_player.FacingDirection, _player.JumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(_stateTimer < 0)
            _stateMachine.ChangeState(_player.AirState);

        if (_player.IsGroundDetected())
            _stateMachine.ChangeState(_player.IdleState);
    }
}
