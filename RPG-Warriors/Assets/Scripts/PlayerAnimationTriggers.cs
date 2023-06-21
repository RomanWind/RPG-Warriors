using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private void AnimationTrigger()
    {
        _player.AnimationTrigger();
    }
}
