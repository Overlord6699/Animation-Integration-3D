using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RagdollInitiator : MonoBehaviour
{
    [SerializeField]
    private float _pushForce;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private List<Rigidbody> _bodyPartsRB;

    public UnityEvent onRagdollStarted;
    public UnityEvent onRagdollEnded;

    private bool _isRagdollEnabled = false;

    public void InitiateRagdoll(HitInfo hit)
    {
        if (_isRagdollEnabled)
            return;

        _isRagdollEnabled = true;
        Rigidbody closest = null;
        var minDistance = FindMinDistance(ref closest, hit);

        onRagdollStarted?.Invoke();
        _animator.enabled = false;
        closest.AddForce(-hit.dirrectionToAttacker * _pushForce, ForceMode.Impulse);
    }

    private float FindMinDistance(ref Rigidbody closest, HitInfo hit)
    {
        var min = float.MaxValue;

        foreach (var part in _bodyPartsRB)
        {
            part.isKinematic = false;
            var distance = Vector3.Distance(hit.position, part.position);

            if (distance < min)
            {
                closest = part;
                min = distance;
            }
        }

        return min;
    }

    public void FinishRagdoll()
    {
        if (!_isRagdollEnabled)
            return;

        _isRagdollEnabled = false;
        foreach (var part in _bodyPartsRB)
        {
            part.isKinematic = true;
        }

        onRagdollEnded?.Invoke();
        _animator.enabled = true;
    }
}
