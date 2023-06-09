using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
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

        if (Input.GetKey(KeyCode.Mouse1) && Unarmed() && _stateMachine._currentState != _player.CatchSwordState)
            _stateMachine.ChangeState(_player.AimSwordState);

        if (Input.GetKey(KeyCode.Q))
            _stateMachine.ChangeState(_player.CounterAttack);

        if (Input.GetKey(KeyCode.Mouse0))
            _stateMachine.ChangeState(_player.PrimaryAttack);

        if(!_player.IsGroundDetected())
            _stateMachine.ChangeState(_player.AirState);

        if(Input.GetKeyDown(KeyCode.Space) && _player.IsGroundDetected())
            _stateMachine.ChangeState(_player.JumpState);
    }

    private bool Unarmed()
    {
        if (!_player.Sword)
            return true;

        _player.Sword.GetComponent<SwordThrowSkillController>().ReturnSword();
        return false;
    }
}
