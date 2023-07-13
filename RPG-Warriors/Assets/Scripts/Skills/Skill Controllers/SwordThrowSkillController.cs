using UnityEngine;

public class SwordThrowSkillController : MonoBehaviour
{
    [SerializeField] private float _returnSpeed = 30f;
    private Animator _swordAnimator;
    private Rigidbody2D _swordRigidbody;
    private CircleCollider2D _swordCollider;
    private Player _player;
    private float _swordDissapearDistance = 1f;
    private bool _canRotate = true;
    private bool _isReturning;


    private void Awake()
    {
        _swordAnimator = GetComponentInChildren<Animator>();
        _swordRigidbody = GetComponent<Rigidbody2D>();
        _swordCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (_canRotate)
            transform.right = _swordRigidbody.velocity;

        if (_isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _returnSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, _player.transform.position) < _swordDissapearDistance)
                _player.CatchTheSword();
        }
    }

    public void SetupSword(Vector2 direction, float gravityScale, Player player)
    {
        _player = player;
        _swordRigidbody.velocity = direction;
        _swordRigidbody.gravityScale = gravityScale;
        _swordAnimator.SetBool("Rotation",true);
    }

    public void ReturnSword()
    {
        _swordRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        _isReturning = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isReturning)
            return;

        _swordAnimator.SetBool("Rotation", false);
        _canRotate = false;
        _swordCollider.enabled = false;

        _swordRigidbody.isKinematic = true;
        _swordRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;
    }
}
