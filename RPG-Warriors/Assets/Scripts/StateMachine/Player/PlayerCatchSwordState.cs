using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    private Transform _sword;
    private float _swordCatchImpact = 3.5f;

    public PlayerCatchSwordState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _sword = _player.Sword.transform;
        if (_player.transform.position.x > _sword.position.x && _player.FacingDirection == 1)
            _player.Flip();
        else if (_player.transform.position.x < _sword.position.x && _player.FacingDirection == -1)
            _player.Flip();

        _player.ModifyXVelocity(_swordCatchImpact * -_player.FacingDirection);
    }
    public override void Update()
    {
        base.Update();

        if (_triggerCalled)
            _stateMachine.ChangeState(_player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
