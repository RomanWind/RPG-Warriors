using UnityEngine;

public class EnemyState
{
    private string _animatorBoolName;

    protected EnemyStateMachine _enemyStateMachine;
    protected Enemy _enemyBase;

    protected bool _triggerCalled;
    protected float _stateTimer;

    public EnemyState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animatorBoolName)
    {
        _enemyStateMachine = enemyStateMachine;
        _enemyBase = enemyBase;
        _animatorBoolName = animatorBoolName;
    }

    public virtual void Update()
    {
        _stateTimer -= Time.deltaTime;
    }

    public virtual void Enter()
    {
        _triggerCalled = false;
        _enemyBase.EnemyAnimator.SetBool(_animatorBoolName, true);
    }

    public virtual void Exit()
    {
        _enemyBase.EnemyAnimator.SetBool(_animatorBoolName, false);
    }
}
