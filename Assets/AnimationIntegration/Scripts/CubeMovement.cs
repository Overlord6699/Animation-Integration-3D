using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float moveDistance;

    private Vector3 moveVelocity;
    private WaitForSeconds wait;

    private Vector3 targetPosition;
    private Vector3 nextPosition;


    private void Start()
    {
        targetPosition = transform.position + transform.forward * moveDistance;
        nextPosition = transform.position;
    }

    private void Update()
    {
        moveVelocity = (targetPosition - transform.position).normalized * moveSpeed;
        if (Vector3.Distance(transform.position, targetPosition) < 0.1)
        {
            var p = targetPosition;
            targetPosition = nextPosition;
            nextPosition = p;
        }
        Debug.DrawLine(transform.position, targetPosition);
    }


    private void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }
}
