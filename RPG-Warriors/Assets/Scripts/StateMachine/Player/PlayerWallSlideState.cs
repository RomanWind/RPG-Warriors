using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    private float _wallSlideSpeedMultiplier = 0.7f;

    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _stateMachine.ChangeState(_player.WallJumpState);
            return;
        }

        if(_inputX != 0 && _player.FacingDirection != _inputX)
            _stateMachine.ChangeState(_player.IdleState);

        if (_inputY < 0)
            _playerRb.velocity = new Vector2(0, _playerRb.velocity.y);
        else
            _playerRb.velocity = new Vector2(0, _playerRb.velocity.y * _wallSlideSpeedMultiplier);

        if(_player.IsGroundDetected())
            _stateMachine.ChangeState(_player.IdleState);

    }
}
