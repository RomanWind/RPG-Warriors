using UnityEngine;

public class Clone : Skill
{
    [SerializeField] private float _cloneDuration;
    [SerializeField] private GameObject _clonePrefab;
    //temporal
    [SerializeField] private bool canAttack;

    public void CreateClone(Transform clonePosition)
    {
        GameObject clone = Instantiate(_clonePrefab);

        clone.GetComponent<CloneSkillController>().SetupClone(clonePosition, _cloneDuration, canAttack);
    }
}
