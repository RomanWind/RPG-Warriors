using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    private EnemySkeleton _skeleton;

    public SkeletonStunnedState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animatorBoolName, EnemySkeleton skeleton) : base(enemyBase, enemyStateMachine, animatorBoolName)
    {
        _skeleton = skeleton;
    }

    public override void Enter()
    {
        base.Enter();

        _stateTimer = _skeleton.GetStunDuration();

        _skeleton.Fx.InvokeRepeating("RedColorBlink", 0, 0.1f);

        _enemyRigidbody.velocity = new Vector2(-_skeleton.FacingDirection * _skeleton.GetStunDirection().x, _skeleton.GetStunDirection().y);
    }

    public override void Update()
    {
        base.Update();

        Debug.Log(_stateTimer);

        if (_stateTimer < 0)
        {
            Debug.Log("Going to idle");
            _enemyStateMachine.ChangeState(_skeleton.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        _skeleton.Fx.Invoke("CancelRedBlink", 0);            
    }
}
