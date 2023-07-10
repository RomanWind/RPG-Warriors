using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private GameObject _player;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this; 
    }

    public GameObject GetPlayer() => _player;
}
