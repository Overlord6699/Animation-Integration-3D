using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KillingTargetSearch : MonoBehaviour, IComparer<Collider>
{
    [SerializeField]
    private ScriptableValueField<float> _searchHeight;

    [SerializeField]
    private float _canKillDistance;

    [SerializeField]
    private LayerMask _killTargetLayerMask;

    [SerializeField]
    private LayerMask _obstackleLayerMask;

    [SerializeField]
    private ScriptableValue<KillingTarget> _killTarget;

    [SerializeField]
    private ScriptableValue<bool> _isFinishing;



    private int _visibilityCheckLayermask;

    private void Start()
    {
        _visibilityCheckLayermask = _killTargetLayerMask.value | _obstackleLayerMask;
    }

    private void OnDisable()
    {
        _killTarget.Value = null;
    }

    private void Update()
    {
        KillingTarget target = null;

        if (!_isFinishing.Value)
            target = GetPossibleFinishingTarget();

        if (_killTarget.Value != target)
        {
            _killTarget.Value = target;
        }
    }

    private KillingTarget GetPossibleFinishingTarget()
    {
        var center = transform.position + Vector3.up * _searchHeight.Value;
        var possibleTargets = new List<Collider>(Physics.OverlapSphere(center,
            _canKillDistance, _killTargetLayerMask));
        possibleTargets.Sort(0, possibleTargets.Count, this);

        foreach (var target in possibleTargets)
        {
            if (!target.TryGetComponent<KillingTarget>(out var finishingTarget))
                continue;
            if (!finishingTarget.CanBeFinished)
                continue;

            var rayDirection = new Vector3(target.transform.position.x, center.y, target.transform.position.z) - center;
            if (Physics.Raycast(center, rayDirection.normalized, out var hit, rayDirection.magnitude, _visibilityCheckLayermask))
            {
                if (hit.collider.transform == target.transform)
                    return finishingTarget;
            }
        }

        return null;
    }

    public int Compare(Collider c1, Collider c2)
    {
        var center = transform.position + Vector3.up * _searchHeight.Value;
        var c1Center = new Vector3(c1.transform.position.x, center.y, c1.transform.position.z);
        var c2Center = new Vector3(c2.transform.position.x, center.y, c2.transform.position.z);

        return Vector3.Distance(c1Center, center).CompareTo(Vector3.Distance(c2Center, center));
    }
}
