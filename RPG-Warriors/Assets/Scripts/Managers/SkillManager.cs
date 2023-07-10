using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    #region skills
    public Dash _dash;
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
    }
}
