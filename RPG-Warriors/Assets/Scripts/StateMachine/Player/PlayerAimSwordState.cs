using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        SkillManager.instance.SwordThrowSkill.DotsActive(true);
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Mouse1))
            _stateMachine.ChangeState(_player.IdleState);

        Vector2 aimMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (_player.transform.position.x > aimMousePosition.x && _player.FacingDirection == 1)
            _player.Flip();
        else if(_player.transform.position.x < aimMousePosition.x && _player.FacingDirection == -1)
            _player.Flip();
    }

    public override void Exit()
    {
        base.Exit();
    }

}
