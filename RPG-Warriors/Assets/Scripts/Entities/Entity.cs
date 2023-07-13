using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator EntityAnimator { get; private set; }
    public Rigidbody2D EntityRigidbody { get; private set; }
    public EntityFX Fx { get; private set; }

    #endregion

    #region Collision
    [Header("Collision info")]
    [SerializeField] protected Transform _groundCheck;
    [SerializeField] protected float _groundCheckDistance;
    [SerializeField] protected Transform _wallCheck;
    [SerializeField] protected float _wallCheckDistance;
    [SerializeField] protected LayerMask _groundLayer;
    #endregion

    #region AttackInfo
    [Header("Attack Info")]
    [SerializeField] protected Transform _attackCheck;
    [SerializeField] protected float _attackCheckRadius;
    public Transform AttackCheck 
    {
        get { return _attackCheck; }
        protected set { _attackCheck = value; } 
    }
    public float AttackCheckRadius
    {
        get { return _attackCheckRadius; }
        protected set { _attackCheckRadius = value; }
    }
    #endregion

    #region SpriteFlip
    public int FacingDirection { get; private set; } = 1;
    protected bool _facingRight = true;
    #endregion

    #region Knockback
    [Header("Knockback Info")]
    [SerializeField] protected Vector2 _knockbackDirection = new Vector2(7,12);
    protected float _knockbackDuration = 0.07f;
    protected bool _isKnocked;
    #endregion

    protected virtual void Awake()
    {
        EntityAnimator = GetComponentInChildren<Animator>();
        EntityRigidbody = GetComponent<Rigidbody2D>();
        Fx = GetComponent<EntityFX>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    #region Combat
    public virtual void Damage()
    {
        Fx.StartCoroutine("FlashFx");
        StartCoroutine(HitKnockback());
    }

    protected virtual IEnumerator HitKnockback()
    {
        _isKnocked = true;
        EntityRigidbody.velocity = new Vector2(_knockbackDirection.x * -FacingDirection, _knockbackDirection.y);
        yield return new WaitForSeconds(_knockbackDuration);
        _isKnocked = false;
    }
    #endregion

    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _groundLayer);
    public virtual bool IsWallDetected() => Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _wallCheckDistance, _groundLayer);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(_groundCheck.position, new Vector3(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance));
        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y));
        Gizmos.DrawWireSphere(_attackCheck.position, _attackCheckRadius);
    }
    #endregion

    #region SpriteFlipMethods
    public void Flip()
    {
        FacingDirection *= -1;
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
    #endregion

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (_isKnocked)
            return;

        EntityRigidbody.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    public void SetZeroVelocity()
    {
        if(_isKnocked)
            return;

        EntityRigidbody.velocity = Vector2.zero;
    }

    public void ModifyXVelocity(float xVelocity)
    {
        EntityRigidbody.velocity = new Vector2(xVelocity, EntityRigidbody.velocity.y);
    }
}
