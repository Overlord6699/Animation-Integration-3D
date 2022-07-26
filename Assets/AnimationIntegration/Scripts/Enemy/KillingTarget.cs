using UnityEngine;
using UnityEngine.Events;

public class KillingTarget : MonoBehaviour
{
    [SerializeField]
    private bool canBeFinished;

    public bool CanBeFinished => canBeFinished;

    public UnityEvent<HitInfo> FinishingStartedEvent;
    public UnityEvent<HitInfo> FinishingEndedEvent;


    public void SetCanBeFinished(bool state)
    {
        canBeFinished = state;
    }

    public void StartFinishing(HitInfo hitInfo)
    {
        Debug.DrawRay(hitInfo.position, hitInfo.attackDirection, Color.red, 5);
        FinishingStartedEvent?.Invoke(hitInfo);
    }

    public void EndFinishing(HitInfo hitInfo)
    {
        Debug.DrawRay(hitInfo.position, hitInfo.attackDirection, Color.yellow, 5);
        FinishingEndedEvent?.Invoke(hitInfo);
    }
}
