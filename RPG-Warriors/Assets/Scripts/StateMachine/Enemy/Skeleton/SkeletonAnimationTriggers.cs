using UnityEngine;

public class SkeletonAnimationTriggers : MonoBehaviour
{
    private EnemySkeleton _skeleton => GetComponentInParent<EnemySkeleton>();

    private void AnimationTrigger()
    {
        _skeleton.AnimationFinishTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_skeleton.AttackCheck.position, _skeleton.AttackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
                hit.GetComponent<Player>().Damage();
        }
    }

    private void OpenCounterWindow() => _skeleton.OpenCounterAttackWindow();
    private void CloseCounterWindow() => _skeleton.CloseCounterAttackWindow();
}
