using UnityEngine;

public class EnemySkeleton : Enemy
{
    #region States
    public SkeletonIdleState IdleState { get; private set; }
    public SkeletonMoveState MoveState { get; private set; }
    public SkeletonBattleState BattleState { get; private set; }
    public SkeletonAttackState AttackState { get; private set; }
    public SkeletonStunnedState StunnedState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        IdleState = new SkeletonIdleState(this, EnemyStMachine, "Idle", this);
        MoveState = new SkeletonMoveState(this, EnemyStMachine, "Move", this);
        BattleState = new SkeletonBattleState(this, EnemyStMachine, "Move", this);
        AttackState = new SkeletonAttackState(this, EnemyStMachine, "Attack", this);
        StunnedState = new SkeletonStunnedState(this, EnemyStMachine, "Stunned", this);
    }

    protected override void Start()
    {
        base.Start();

        EnemyStMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanBeStunned()
    {
        if(base.CanBeStunned())
        {
            EnemyStMachine.ChangeState(StunnedState); 
            return true;
        }

        return false;
    }
}
