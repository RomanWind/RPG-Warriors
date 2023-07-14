using System.Collections.Generic;
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

    //Sword Pierce
    private float _piercesAmount;

#region Sword Bounce
    private List<Transform> _enemyTarget;
    private int _amountOfBounces;
    private int _targetIndex;
    private float _swordBounceRadius = 10f;
    private float _swordBounceSpeed = 20f;
    private float _swordBoundeDistance = 0.25f;
    private bool _isBouncing;
#endregion

#region Sword Spin
    private float _maxTravelDistance;
    private float _spinDuration;
    private float _spinTimer;
    private float _swordSpinRadius = 1f;
    private bool _wasStopped;
    private bool _isSpinning;

    private float _hitTimer;
    private float _hitCooldown;
#endregion

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
            if (Vector2.Distance(transform.position, _player.transform.position) < _swordDissapearDistance)
                _player.CatchTheSword();
        }

        BounceLogic();

        SpinLogic();
    }

    private void SpinLogic()
    {
        if (_isSpinning)
        {
            if (Vector2.Distance(_player.transform.position, transform.position) > _maxTravelDistance && !_wasStopped)
                StopWhenSpinning();

            if (_wasStopped)
            {
                _spinTimer -= Time.deltaTime;
                if (_spinTimer < 0)
                {
                    _isReturning = true;
                    _isSpinning = false;
                }

                _hitTimer -= Time.deltaTime;

                if (_hitTimer <= 0)
                {
                    _hitTimer = _hitCooldown;
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _swordSpinRadius);

                    foreach (var hit in colliders)
                    {
                        if (hit.TryGetComponent<Enemy>(out Enemy enemy))
                            enemy.Damage();
                    }
                }
            }
        }
    }

    private void StopWhenSpinning()
    {
        _wasStopped = true;
        _swordRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        _spinTimer = _spinDuration;
    }

    private void BounceLogic()
    {
        if (_isBouncing && _enemyTarget.Count > 0)
        {
            Debug.Log("bouncing");
            transform.position = Vector2.MoveTowards(transform.position, _enemyTarget[_targetIndex].position, _swordBounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _enemyTarget[_targetIndex].position) < _swordBoundeDistance)
            {
                _enemyTarget[_targetIndex].GetComponent<Enemy>().Damage();
                _targetIndex++;
                _amountOfBounces--;

                if (_amountOfBounces <= 0)
                {
                    _isBouncing = false;
                    _isReturning = true;
                }

                if (_targetIndex >= _enemyTarget.Count)
                    _targetIndex = 0;
            }
        }
    }

    private void SwordBounceMechanic(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out _))
        {
            if(_isBouncing && _enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _swordBounceRadius);

                foreach(var hit in colliders)
                {
                    if (hit.TryGetComponent<Enemy>(out _))
                        _enemyTarget.Add(hit.transform);
                }
            }
        }
    }

    public void SetupSword(Vector2 direction, float gravityScale, Player player)
    {
        _player = player;
        _swordRigidbody.velocity = direction;
        _swordRigidbody.gravityScale = gravityScale;

        if(_piercesAmount <= 0)
            _swordAnimator.SetBool("Rotation",true);
    }

    public void SetupBounce(bool isBouncing, int amountOfBounces)
    {
        _isBouncing = isBouncing;
        _amountOfBounces = amountOfBounces;
        _enemyTarget = new List<Transform>();
    }

    public void SetupPierce(int piercesAmount)
    {
        _piercesAmount = piercesAmount;
    }

    public void SetupSpin(bool isSpinning, float maxTravelDistance, float spinDuration, float hitCooldown)
    {
        _isSpinning = isSpinning;
        _maxTravelDistance = maxTravelDistance;
        _spinDuration = spinDuration;
        _hitCooldown = hitCooldown;
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

        collision.GetComponent<Enemy>()?.Damage();

        SwordBounceMechanic(collision);
        StuckInto(collision);
    }

    private void StuckInto(Collider2D collision)
    {
        if (_piercesAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            _piercesAmount--;
            return;
        }

        if (_isSpinning)
        {
            StopWhenSpinning();
            return;
        }

        _canRotate = false;
        _swordCollider.enabled = false;

        _swordRigidbody.isKinematic = true;
        _swordRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        if (_isBouncing && _enemyTarget.Count > 0)
            return;

        _swordAnimator.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }
}
