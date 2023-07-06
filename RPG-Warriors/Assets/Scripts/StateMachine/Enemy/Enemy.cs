using UnityEngine;

public class Enemy : Entity
{
    private float _playerDetectDistance = 10f;
    [SerializeField] protected LayerMask _whatIsPlayer;

    [Header("Behaviour Info")]
    [SerializeField] private float _movementSpeed = 2f;
    [SerializeField] private float _idleTime = 1f;
    [SerializeField] private float _battleTime = 4f;
    [SerializeField] private float _agroDistance = 7f;
    [SerializeField] private float _attackDistance = 1.5f;
    [SerializeField] private float _attackCooldown = 1f;
    [HideInInspector] private float _lastTimeAttacked;

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
    public float GetBattleTime() => _battleTime;
    public float GetAgroDistance () => _agroDistance;
    public float GetAttackDistance() => _attackDistance;
    public float GetAttackCooldown() => _attackCooldown;
    public float GetLastTimeAttacked() => _lastTimeAttacked;

    public void SetLastAttackTime(float lastTimeAttacked)
    {
        _lastTimeAttacked = lastTimeAttacked;
    }

    public virtual void AnimationFinishTrigger() => EnemyStMachine._currentEnemyState.AnimationFinishTrigger();
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _playerDetectDistance, _whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + _attackDistance * FacingDirection, transform.position.y));
    }
}
