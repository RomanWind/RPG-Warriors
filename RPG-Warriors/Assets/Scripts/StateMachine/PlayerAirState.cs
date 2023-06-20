using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
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

        if (_player.IsWallDetected())
            _stateMachine.ChangeState(_player.WallSlideState);

        if(_player.IsGroundDetected())
            _stateMachine.ChangeState(_player.IdleState);

        float speedReduction = 0.8f;
        if(_inputX != 0)
        {
            _player.SetVelocity(_player.MovementSpeed * speedReduction * _inputX, _playerRb.velocity.y);
        }
    }
}
