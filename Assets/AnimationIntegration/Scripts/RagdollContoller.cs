using UnityEngine;

public class RagdollContoller : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody[] _allRigidBodies;

    private void Awake()
    {
        foreach(Rigidbody rb in _allRigidBodies)
        {
            rb.isKinematic = true;
        }
    }

    public void EnableRagdoll()
    {
        _animator.enabled = false;

        foreach(Rigidbody rb in _allRigidBodies)
        {
            rb.isKinematic = false;
        }

    }
}
