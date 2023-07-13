using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private GameObject _player;
    private Player _playerScript;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        _playerScript = _player.GetComponent<Player>();
    }

    public GameObject GetPlayer() => _player;
    public Player GetPlayerScript() => _playerScript;
}
