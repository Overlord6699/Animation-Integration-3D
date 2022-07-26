using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody _RB;


    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;

    public UnityEvent OnSpacePressed;

    private Vector3 _moveVector
        ;

    private void Update()
    {
        var horizontalDelta = Input.GetAxis("Horizontal");
        var verticalDelta = Input.GetAxis("Vertical");


        _moveVector = new Vector3(-verticalDelta, 0, horizontalDelta);
        var clampedMove = Vector3.ClampMagnitude(_moveVector, 1);

        _animator.SetFloat("Speed", clampedMove.magnitude);
        //ограничение до 1 при движении по диагонали
        _RB.velocity = _speed * clampedMove;

        Rotate();

        if (Input.GetKeyDown(KeyCode.Space))
            OnSpacePressed?.Invoke();

    }

    private void Rotate()
    {
        if (_moveVector.magnitude > 0.1f)
        {
            _target.rotation = Quaternion.Lerp(
                _target.rotation,
                Quaternion.LookRotation(_moveVector),
                Time.deltaTime * _rotationSpeed
            );
        }
    }
}
