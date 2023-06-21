using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region AttackDetails
    [SerializeField] private float[] _attackMovement;
    public float[] AttackMovement { get; private set; }

    #endregion

    #region Movement
    public float MovementSpeed { get; private set; } = 15f;
    public float JumpForce { get; private set; } = 13f;

    #endregion
    #region Dash
    public float DashSpeed { get; private set; } = 25f;
    public float DashDuration { get; private set; } = 0.25f;
    public float DashDirection { get; private set; } = 1;

    private float _dashCooldown = 1.25f;
    private float _dashUsageTimer;

    #endregion
    #region Collision
    [Header("Collision info")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private LayerMask _groundLayer;

    #endregion

    public bool IsBusy { get; private set; }
    public int FacingDirection { get; private set; } = 1;
    private bool _facingRight = true;

    #region Components
    public Animator PlayerAnimator { get; private set; }
    public Rigidbody2D PlayerRb { get; private set; }

    #endregion

    #region States
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    //Attack states
    public PlayerPrimaryAttackState PrimaryAttack { get; private set; }

    #endregion

    private void Awake()
    {
        PlayerAnimator = GetComponentInChildren<Animator>();
        PlayerRb = GetComponent<Rigidbody2D>();
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
        PrimaryAttack = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);

        AttackMovement = new float[10];
        for(int i = 0; i < _attackMovement.Length; i++)
        {
            AttackMovement[i] = _attackMovement[i];
        }
    }

    private void Update()
    {
        StateMachine._currentState.Update();

        CheckForDashInput();
    }

    public IEnumerator BusyTimer(float _seconds)
    {
        IsBusy = true;

        yield return new WaitForSeconds(_seconds);

        IsBusy = false;
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        PlayerRb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _groundLayer);
    public bool IsWallDetected() => Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _wallCheckDistance, _groundLayer);
    public void AnimationTrigger() => StateMachine._currentState.AnimationFinishTrigger();

    public void Flip()
    {
        FacingDirection = FacingDirection * -1;
        _facingRight = !_facingRight;

        /*Rotation of sprite is not good decigion in general but due to sprite quality (player is not centered) i can't use SpriteRenderer.flipX or flipY cause sprite will change position and collider gonna stay at the same spot
        That means i will need to rotate/move collider anyway to make visual/physics parts of player to match*/
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float x)
    {
        if (x > 0 && !_facingRight)
            Flip();
        else if (x < 0 && _facingRight)
            Flip();
    }

    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        _dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && _dashUsageTimer < 0)
        {
            _dashUsageTimer = _dashCooldown;
            DashDirection = Input.GetAxisRaw("Horizontal");
            if (DashDirection == 0)
                DashDirection = FacingDirection;

            StateMachine.ChangeState(DashState);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_groundCheck.position, new Vector3(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance));
        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y));
    }
}
