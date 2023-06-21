using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine _stateMachine;
    protected Player _player;
    protected Rigidbody2D _playerRb;

    private string _animatorBoolName;
    protected float _inputX;
    protected float _inputY;

    protected float _stateTimer;
    protected bool _triggerCalled;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animatorBoolName)
    {
        _player = player;
        _animatorBoolName = animatorBoolName;
        _stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        _player.EntityAnimator.SetBool(_animatorBoolName, true);
        _playerRb = _player.EntityRigidbody;
        _triggerCalled = false;
    }

    public virtual void Update()
    {
        _stateTimer -= Time.deltaTime;

        _inputX = Input.GetAxisRaw("Horizontal");
        _inputY = Input.GetAxisRaw("Vertical");
        _player.EntityAnimator.SetFloat("yVelocity", _playerRb.velocity.y);
    }

    public virtual void Exit()
    {
        _player.EntityAnimator.SetBool(_animatorBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        _triggerCalled = true;
    }
}
