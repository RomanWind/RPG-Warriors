using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator _animator { get; private set; }
    public PlayerStateMachine _stateMachine { get; private set; }

    public PlayerIdleState _idleState { get; private set; }
    public PlayerMoveState _moveState { get; private set; }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _stateMachine = new PlayerStateMachine();

        _idleState = new PlayerIdleState(this, _stateMachine, "Idle");
        _moveState = new PlayerMoveState(this, _stateMachine, "Move");
    }

    private void Start()
    {
        _stateMachine.Initialize(_idleState);
    }

    private void Update()
    {
        _stateMachine._currentState.Update();
    }
}
