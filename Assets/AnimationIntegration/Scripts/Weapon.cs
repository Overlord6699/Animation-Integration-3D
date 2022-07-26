using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform _owner;
    [SerializeField]
    private float _distance;
    [SerializeField]
    private float _force;

    private RaycastHit _hit;


    private void Update()
    {
        CheckTarget();
    }

    public void CheckTarget()
    {
        if (Physics.Raycast(
               _owner.position,
               _owner.TransformDirection(Vector3.forward),
               out _hit, _distance))
        {
            if (_hit.collider.tag == "Enemy")
            {
                Debug.Log("LOL");
                EnableEnemyRagdoll();

                _hit.collider.gameObject.GetComponent<Rigidbody>().
                    AddForce(_owner.transform.TransformDirection(Vector3.forward) * _force);
            }
        }
    }

    private void EnableEnemyRagdoll()
    {
        _hit.collider.gameObject.GetComponent<RagdollContoller>().EnableRagdoll();
    }
}
