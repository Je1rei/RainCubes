using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Sphere : Shape<Sphere>
{
    [SerializeField] private Material _invisibleMaterial;
    [SerializeField] private Material _defaultMaterial;

    private void OnEnable()
    {
        ActivateCoroutine();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    protected override void Initialize()
    {
        SetMaterial(_defaultMaterial);
        base.Initialize();

        Color color = Material.color;
        color.a = 1f;

        SetColor(color);
    }

    protected override IEnumerator LifeRoutine()
    {
        Initialize();

        FadeMaterial();

        yield return base.LifeRoutine();

        SetMaterial(_invisibleMaterial);
    }
}