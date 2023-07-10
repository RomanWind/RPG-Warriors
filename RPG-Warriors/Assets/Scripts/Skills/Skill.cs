using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float _cooldown;
    protected float _cooldownTimer;

    protected virtual void Update()
    {
        _cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(_cooldownTimer < 0)
        {
            UseSkill();
            _cooldownTimer = _cooldown;
            return true;
        }

        Debug.Log("Skill is on CD");
        return false;
    }

    public virtual void UseSkill()
    {

    }
}
