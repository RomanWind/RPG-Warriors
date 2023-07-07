using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    

    [Header("Flash FX")]
    [SerializeField] private Material _hitMaterial;
    private Material _originalMaterial;
    private SpriteRenderer _spriteRenderer;
    private float _flashDuration = 0.2f;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalMaterial = _spriteRenderer.material;
    }

    private IEnumerator FlashFx()
    {
        _spriteRenderer.material = _hitMaterial;
        yield return new WaitForSeconds(_flashDuration);
        _spriteRenderer.material = _originalMaterial;
    }
}
