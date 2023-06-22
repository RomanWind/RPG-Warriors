using UnityEngine;

public class EnemySkeleton : Enemy
{
    #region States
    public SkeletonIdleState SkeletonIdleSt { get; private set; }
    public SkeletonMoveState SkeletonMoveSt { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        SkeletonIdleSt = new SkeletonIdleState(this, EnemyStMachine, "Idle", this);
        SkeletonMoveSt = new SkeletonMoveState(this, EnemyStMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();

        EnemyStMachine.Initialize(SkeletonIdleSt);
    }

    protected override void Update()
    {
        base.Update();
    }
}
