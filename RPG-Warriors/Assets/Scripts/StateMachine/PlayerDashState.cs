using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _stateTimer = _player.DashDuration;
    }

    public override void Exit()
    {
        base.Exit();

        _player.SetVelocity(0, _playerRb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if(!_player.IsGroundDetected() && _player.IsWallDetected())
            _stateMachine.ChangeState(_player.WallSlideState);

        _player.SetVelocity(_player.DashSpeed * _player.DashDirection, 0);

        if(_stateTimer < 0)
            _stateMachine.ChangeState(_player.IdleState);
    }
}
