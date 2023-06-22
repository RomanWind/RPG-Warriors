using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator EntityAnimator { get; private set; }
    public Rigidbody2D EntityRigidbody { get; private set; }

    #endregion

    #region Collision
    [Header("Collision info")]
    [SerializeField] protected Transform _groundCheck;
    [SerializeField] protected float _groundCheckDistance;
    [SerializeField] protected Transform _wallCheck;
    [SerializeField] protected float _wallCheckDistance;
    [SerializeField] protected LayerMask _groundLayer;
    #endregion

    #region SpriteFlip
    public int FacingDirection { get; private set; } = 1;
    protected bool _facingRight = true;
    #endregion

    protected virtual void Awake()
    {
        EntityAnimator = GetComponentInChildren<Animator>();
        EntityRigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _groundLayer);
    public virtual bool IsWallDetected() => Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _wallCheckDistance, _groundLayer);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(_groundCheck.position, new Vector3(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance));
        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y));
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
        EntityRigidbody.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
}
