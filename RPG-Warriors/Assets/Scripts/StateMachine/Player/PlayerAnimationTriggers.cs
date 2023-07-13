using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private void AnimationTrigger()
    {
        _player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_player.AttackCheck.position, _player.AttackCheckRadius);

        foreach(var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
                hit.GetComponent<Enemy>().Damage();
        }
    }

    private void ThrowSword()
    {
        SkillManager.instance.SwordThrowSkill.CreateSword();
    }
}
