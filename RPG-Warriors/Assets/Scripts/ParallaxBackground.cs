using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _parallaxEffect;
    private float _positionX;
    private float _length;

    void Start()
    {
        _positionX = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distanceMoved = _camera.transform.position.x * (1 - _parallaxEffect);
        float distanceToMove = _camera.transform.position.x * _parallaxEffect;

        transform.position = new Vector3(_positionX + distanceToMove, transform.position.y);

        if (distanceMoved > _positionX + _length)
            _positionX += _length;
        else if (distanceMoved < _positionX - _length)
            _positionX -= _length;
    }
}
