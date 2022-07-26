using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishingBehaviour : MonoBehaviour
{
    [SerializeField]
    private ScriptableValue<float> searchHeight;

    [SerializeField]
    private ScriptableValue<KillingTarget> finishTargetSelector;

    [SerializeField]
    private ScriptableValue<bool> isFinishing;

    [SerializeField]
    private float finishTeleportationDistance;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private List<MonoBehaviour> onKillDisableObjects;

    [SerializeField]
    private Transform attackPoint;

    [SerializeField]
    private float dashTime;

    public UnityEvent onKillStarted;
    public UnityEvent onKillEnded;




    private KillingTarget _currentTarget;
    private int _hitTriggerId;
    private Vector3 _hitDirection;
    private WaitForFixedUpdate _waitCor;


    private void Awake()
    {
        _hitTriggerId = Animator.StringToHash("Finish");
        _waitCor = new WaitForFixedUpdate();
    }

    private void StartFinishing(KillingTarget currentTarget)
    {
        if (currentTarget == null || isFinishing.Value)
            return;

        isFinishing.Value = true;
        _currentTarget = currentTarget;      

        foreach (var c in onKillDisableObjects)
        {
            c.enabled = false;
        }

        var center = new Vector3(transform.position.x, 
            transform.position.y + searchHeight.Value,
            transform.position.z);
        var target = new Vector3(currentTarget.transform.position.x, center.y, currentTarget.transform.position.z);

        var dirToCenter = center - target;
        _hitDirection = dirToCenter.normalized;

        
        rb.MoveRotation(Quaternion.LookRotation(-dirToCenter.normalized, Vector3.up));        
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;

        onKillStarted?.Invoke();
        animator.SetTrigger(_hitTriggerId);
    }

    private void Update()
    {
        if (!isFinishing.Value && finishTargetSelector.Value != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                StartFinishing(finishTargetSelector.Value);
        }
        if (isFinishing.Value)
        {
            rb.velocity = Vector3.zero;
        }
    }

    #region Animation Events
    //DON'T RENAME

    public void AttackPrepared()
    {
        var center = new Vector3(transform.position.x,
            transform.position.y + searchHeight.Value,
            transform.position.z);
        var target = new Vector3(_currentTarget.transform.position.x, center.y, _currentTarget.transform.position.z);
        var dirToCenter = center - target;

        StartCoroutine(DashToTarget(target + dirToCenter.normalized * finishTeleportationDistance - Vector3.up * searchHeight.Value));
    }

    public void EnemyPinned()
    {
        if (!isFinishing.Value)
            return;
        _currentTarget.StartFinishing(new HitInfo()
        {
            position = attackPoint.position,
            dirrectionToAttacker = _hitDirection,
            attackDirection = attackPoint.forward
        });
    }

    public void EnemyDead()
    {
        if (!isFinishing.Value)
            return;
        _currentTarget.EndFinishing(new HitInfo()
        {
            position = attackPoint.position,
            dirrectionToAttacker = _hitDirection,
            attackDirection = -attackPoint.forward
        });
    }

    public void FinishingEnded()
    {
        if (!isFinishing.Value)
            return;
        isFinishing.Value = false;
        _currentTarget = null;
        onKillEnded?.Invoke();
        foreach (var c in onKillDisableObjects)
        {
            c.enabled = true;
        }
    }

    #endregion


    private IEnumerator DashToTarget(Vector3 target)
    {
        var distance = target - rb.position;
        var numOfSteps = Mathf.Max(dashTime / Time.fixedDeltaTime, 1);
        var step = distance / numOfSteps;
        for (var i = 0; i < numOfSteps; i++)
        {
            rb.MovePosition(rb.position + step);
            yield return _waitCor;
        }
    }

    private void OnDestroy()
    {
        isFinishing.Value = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (searchHeight == null)
            return;
        Gizmos.color = Color.yellow;
        var target =transform.position + transform.up * searchHeight.Value + transform.forward * finishTeleportationDistance;
        Gizmos.DrawLine(transform.position + Vector3.up * searchHeight.Value, target);
        Gizmos.DrawSphere(target, 0.1f);
    }
}
