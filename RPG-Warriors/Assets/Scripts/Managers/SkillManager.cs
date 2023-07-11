using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    #region skills
    public Dash _dash;
    public Clone _clone;
    #endregion

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);

        instance = this; 
    }

    private void Start()
    {
        _dash = GetComponent<Dash>();
        _clone = GetComponent<Clone>();
    }
}
