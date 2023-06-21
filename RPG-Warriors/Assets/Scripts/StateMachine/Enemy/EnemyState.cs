using UnityEngine;

public class EnemyState
{
    private string _animatorBoolName;

    protected EnemyStateMachine _enemyStateMachine;
    protected Enemy _enemy;

    protected bool _triggerCalled;
    protected float _stateTimer;

    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animatorBoolName)
    {
        _enemyStateMachine = enemyStateMachine;
        _enemy = enemy;
        _animatorBoolName = animatorBoolName;
    }

    public virtual void Update()
    {
        _stateTimer -= Time.deltaTime;
    }

    public virtual void Enter()
    {
        _triggerCalled = false;
        _enemy.EnemyAnimator.SetBool(_animatorBoolName, true);
    }

    public virtual void Exit()
    {
        _enemy.EnemyAnimator.SetBool(_animatorBoolName, false);
    }
}
