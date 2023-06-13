using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
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

        _player.SetVelocity(_inputX * _player.MovementSpeed, _playerRb.velocity.y);

        if (_inputX == 0 || _player.IsWallDetected())
            _stateMachine.ChangeState(_player.IdleState);
    }
}
