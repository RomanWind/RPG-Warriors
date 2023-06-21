using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState _currentEnemyState { get; private set; }

    public void Initialize(EnemyState startState)
    {
        _currentEnemyState = startState;
        _currentEnemyState.Enter();
    }

    public void ChangeState(EnemyState newState)
    {
        _currentEnemyState.Exit();
        _currentEnemyState = newState;
        _currentEnemyState.Enter();
    }
}
