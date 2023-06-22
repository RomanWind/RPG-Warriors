using UnityEngine;

public class Enemy : Entity
{
    [Header("Behaviour Info")]
    [SerializeField] protected float _movementSpeed = 2f;
    [SerializeField] protected float _idleTime = 1f;

    public Rigidbody2D EnemyRigidbody { get; private set; }
    public Animator EnemyAnimator { get; private set; }

    public EnemyStateMachine EnemyStMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        EnemyRigidbody = EntityRigidbody;
        EnemyAnimator = EntityAnimator;

        EnemyStMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        EnemyStMachine._currentEnemyState.Update();
    }

    public float GetMovementSpeed() => _movementSpeed;
    public float GetIdleTime() => _idleTime;
}
