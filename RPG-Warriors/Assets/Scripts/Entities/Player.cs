using System.Collections;
using UnityEngine;

public class Player : Entity
{
    #region AttackDetails
    [Header("Attack Details")]
    [SerializeField] private float[] _attackMovement;
    public float[] AttackMovement { get; private set; }
    public float CounterAttackDuration { get; private set; } = 0.2f;
    #endregion

    #region Movement
    public float MovementSpeed { get; private set; } = 9f;
    public float JumpForce { get; private set; } = 13f;
    #endregion

    #region Dash
    public float DashSpeed { get; private set; } = 25f;
    public float DashDuration { get; private set; } = 0.25f;
    public float DashDirection { get; private set; } = 1;
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
    public PlayerCounterAttackState CounterAttack { get; private set; }
    //Skill states
    public PlayerAimSwordState AimSwordState { get; private set; }
    public PlayerCatchSwordState CatchSwordState { get; private set; }
    #endregion

    public bool IsBusy { get; private set; }
    public GameObject Sword { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
        PrimaryAttack = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
        CounterAttack = new PlayerCounterAttackState(this, StateMachine, "CounterAttack");
        AimSwordState = new PlayerAimSwordState(this, StateMachine, "AimSword");
        CatchSwordState = new PlayerCatchSwordState(this, StateMachine,"CatchSword");
    }

    protected override void Start()
    {
        base.Start();

        StateMachine.Initialize(IdleState);
        AttackMovement = new float[10];
        for(int i = 0; i < _attackMovement.Length; i++)
        {
            AttackMovement[i] = _attackMovement[i];
        }
    }

    protected override void Update()
    {
        base.Update();

        StateMachine._currentState.Update();
        CheckForDashInput();
    }

    public void AssignNewSword(GameObject newSword)
    {
        Sword = newSword;
    }
    public void CatchTheSword()
    {
        StateMachine.ChangeState(CatchSwordState);
        Destroy(Sword);
    }

    public IEnumerator BusyTimer(float _seconds)
    {
        IsBusy = true;

        yield return new WaitForSeconds(_seconds);

        IsBusy = false;
    }

    public void AnimationTrigger() => StateMachine._currentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.DashSkill.CanUseSkill())
        {
            DashDirection = Input.GetAxisRaw("Horizontal");
            if (DashDirection == 0)
                DashDirection = FacingDirection;

            StateMachine.ChangeState(DashState);
        }
    }
}
