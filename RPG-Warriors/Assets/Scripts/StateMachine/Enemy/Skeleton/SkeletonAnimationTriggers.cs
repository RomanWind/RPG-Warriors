using UnityEngine;

public class SkeletonAnimationTriggers : MonoBehaviour
{
    private EnemySkeleton _skeleton => GetComponentInParent<EnemySkeleton>();

    private void AnimationTrigger()
    {
        _skeleton.AnimationFinishTrigger();
    }
}
