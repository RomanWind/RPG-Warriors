using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int _comboCounter;
    private float _lastAttackTime;
    private float _comboResetTime = 1f;
    private float _blockingMovementTime = 0.05f;

    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        _inputX = 0; //sometimes player use _inputX from previous sequence of attacks and mess up attack direction so we clear _inputX to fix bug with wrong attack direction

        if (_comboCounter > 2 || Time.time >= _lastAttackTime + _comboResetTime)
            _comboCounter = 0;

        _player.EntityAnimator.SetInteger("ComboCounter", _comboCounter);

        float attackDirection = _player.FacingDirection;
        if (_inputX != 0)
            attackDirection = _inputX;

        _player.SetVelocity(_player.AttackMovement[_comboCounter] * attackDirection, _playerRb.velocity.y);

        _stateTimer = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();

        _player.StartCoroutine("BusyTimer", _blockingMovementTime);

        _comboCounter++;
        _lastAttackTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if(_stateTimer < 0)
            _playerRb.velocity = Vector2.zero;

        if(_triggerCalled)
            _stateMachine.ChangeState(_player.IdleState);
    }
}
