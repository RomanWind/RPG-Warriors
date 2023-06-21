using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _playerRb.velocity = new Vector2(_playerRb.velocity.x, _player.JumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (_playerRb.velocity.y < 0)
            _stateMachine.ChangeState(_player.AirState);
    }
}
