using System;
using Unity.Mathematics;
using UnityEngine;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class SwordThrow : Skill
{
    public SwordType _swordType = SwordType.Regular;

    [Header("BounceInfo")]
    [SerializeField] private int _bouncesAmount;
    [SerializeField] private float _bounceGravity;

    [Header("Pierce Info")]
    [SerializeField] private int _piercesAmount;
    [SerializeField] private float _pierceGravity;

    [Header("Spin Info")]
    [SerializeField] private float _maxTravelDistance = 7f;
    [SerializeField] private float _spinDuration = 2f;
    [SerializeField] private float _spinGravity = 1f;
    [SerializeField] private float _hitCooldown = 0.25f;

    [Header("Skill Info")]
    [SerializeField] private GameObject _swordPrefab;
    [SerializeField] private Vector2 _launchForce;
    [SerializeField] private float _swordGravity;

    [Header("Aim Dots")]
    [SerializeField] private GameObject _dotPrefab;
    [SerializeField] private int _numberOfDots;
    [SerializeField] private float _spaceBetweenDots;
    [SerializeField] private Transform _dotsParent;
    private GameObject[] _dots;

    private Vector2 _finalDirection;

    protected override void Start()
    {
        base.Start();

        GenerateDots();
        SetupGravity();
    }

    private void SetupGravity()
    {
        switch(_swordType)
        {
            case SwordType.Bounce:
                _swordGravity = _bounceGravity;
            break;
            case SwordType.Pierce:
                _swordGravity = _pierceGravity;
            break;
            case SwordType.Spin:
                _swordGravity = _spinGravity;
            break;
        }
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            _finalDirection = new Vector2(AimDirection().normalized.x * _launchForce.x, AimDirection().normalized.y * _launchForce.y);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < _dots.Length; i++)
            {
                _dots[i].transform.position = DotsPosition(i * _spaceBetweenDots);
            }
        }
    }

    public void CreateSword()
    {
        GameObject newSword = Instantiate(_swordPrefab, _player.transform.position, _player.transform.rotation);
        SwordThrowSkillController newSwordScript = newSword.GetComponent<SwordThrowSkillController>();

        switch(_swordType)
        {
            case SwordType.Bounce:
                newSwordScript.SetupBounce(true, _bouncesAmount);
            break;
            case SwordType.Pierce:
                newSwordScript.SetupPierce(_piercesAmount);
            break;
            case SwordType.Spin:
                newSwordScript.SetupSpin(true, _maxTravelDistance, _spinDuration, _hitCooldown);
            break;
        }

        newSwordScript.SetupSword(_finalDirection, _swordGravity, _player);
        _player.AssignNewSword(newSword);
        DotsActive(false);
    }

    #region Aim
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = _player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsActive(bool isActive)
    {
        for (int i = 0; i < _dots.Length; i++)
        {
            _dots[i].SetActive(isActive);
        }
    }

    private void GenerateDots()
    {
        _dots = new GameObject[_numberOfDots];

        for (int i = 0; i < _numberOfDots; i++)
        {
            _dots[i] = Instantiate(_dotPrefab, _player.transform.position, Quaternion.identity, _dotsParent);
            _dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)_player.transform.position + new Vector2(
            AimDirection().normalized.x * _launchForce.x,
            AimDirection().normalized.y * _launchForce.y) * t + 0.5f * (Physics2D.gravity * _swordGravity) * math.pow(t,2);

        return position;
    }
    #endregion
}
