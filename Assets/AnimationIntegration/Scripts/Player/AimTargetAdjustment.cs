using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTargetAdjustment : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform center;

    [SerializeField]
    private Transform target;
    

    private void Update()
    {
        var heightDiff = transform.position.y - center.position.y;
        var mouseRayDirection = cam.ScreenPointToRay(Input.mousePosition).direction;
        var angle = Vector3.Angle(mouseRayDirection, Vector3.down);

        target.position = transform.position + mouseRayDirection * heightDiff / Mathf.Cos(angle * Mathf.Deg2Rad);
    }
}
