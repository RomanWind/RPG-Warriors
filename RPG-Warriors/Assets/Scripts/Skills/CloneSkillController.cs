using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]private SpriteRenderer _cloneSprite;
    [SerializeField]private Animator _cloneAnimator;
    [SerializeField] private Transform _attackCheck;

    [Header("Clone Skill Settings")]
    [SerializeField] private float _cloneDisappearSpeed;
    private float _attackCheckRadius = 0.8f;
    private float _cloneAgroRadius = 15f;
    private float _cloneTimer;
    private Transform _closestEnemyPosition;

    private void Update()
    {
        _cloneTimer -= Time.deltaTime;

        if(_cloneTimer < 0)
        {
            _cloneSprite.color = new Color(1, 1, 1, _cloneSprite.color.a - (Time.deltaTime * _cloneDisappearSpeed));

            if(_cloneSprite.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetupClone(Transform newTransform, float cloneDuration, bool canAttack)
    {
        if(canAttack)
        {
            _cloneAnimator.SetInteger("AttackCounter", Random.Range(1,3));
        }
        transform.position = newTransform.position;
        _cloneTimer = cloneDuration;
        FaceClosestTarget();
    }

    private void AnimationTrigger()
    {
        _cloneTimer = -0.1f;
    }
    
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackCheck.position, _attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
                hit.GetComponent<Enemy>().Damage();
        }
    }

    private void FaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, _cloneAgroRadius);
        float closestDistance = Mathf.Infinity;
        _closestEnemyPosition = null;

        foreach (var hit in colliders) 
        {
            if(hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    _closestEnemyPosition = hit.transform;
                }
            }   
        }

        if (_closestEnemyPosition != null)
        {
            if (transform.position.x > _closestEnemyPosition.position.x)
                gameObject.transform.Rotate(0, 180, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, _cloneAgroRadius);
    }
}
