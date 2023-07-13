using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    #region Skills
    public Dash DashSkill { get; private set; }
    public Clone CloneSkill { get; private set; }
    public SwordThrow SwordThrowSkill { get; private set; }
    #endregion

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);

        instance = this; 
    }

    private void Start()
    {
        DashSkill = GetComponent<Dash>();
        CloneSkill = GetComponent<Clone>();
        SwordThrowSkill = GetComponent<SwordThrow>();
    }
}
