using UnityEngine;

//[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private float stopThreshold;

    [SerializeField]
    private ScriptableValue<float> maxVelocity;

    private int _isMovingParam;
    private int _xDirectionParam;
    private int _yDirectionParam;
    private int _velocityParam;

    private float _RBSpeed;
    private Vector3 _RBVelocity;

    private void Awake()
    {
        //для оптимизации
        GetAnimatorParameters();
    }

    private void GetAnimatorParameters()
    {
        _xDirectionParam = Animator.StringToHash("xMoveDir");
        _yDirectionParam = Animator.StringToHash("yMoveDir");
        _velocityParam = Animator.StringToHash("velocity");
        _isMovingParam = Animator.StringToHash("isMoving");
    }

    private void LateUpdate()
    {
        _RBSpeed = _rb.velocity.magnitude;
        _RBVelocity = _rb.velocity.normalized;

        var x = Vector3.Dot(transform.right, _RBVelocity);
        var y = Vector3.Dot(transform.forward, _RBVelocity);
        
        _animator.SetFloat(_xDirectionParam, x);
        _animator.SetFloat(_yDirectionParam, y);
        //как минимум будет 1
        _animator.SetFloat(_velocityParam, Mathf.Min(_RBSpeed / maxVelocity.Value, 1));
        _animator.SetBool(_isMovingParam, _RBSpeed > stopThreshold);
    }
}
