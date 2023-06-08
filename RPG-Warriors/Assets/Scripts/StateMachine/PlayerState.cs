using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine _stateMachine;
    protected Player _player;

    private string _animatorBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animatorBoolName)
    {
        _player = player;
        _animatorBoolName = animatorBoolName;
        _stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        Debug.Log("Enter: " + _animatorBoolName);
    }

    public virtual void Update()
    {
        Debug.Log("Currently in: " + _animatorBoolName);
    }

    public virtual void Exit()
    {
        Debug.Log("Exit: " + _animatorBoolName);
    }
}
