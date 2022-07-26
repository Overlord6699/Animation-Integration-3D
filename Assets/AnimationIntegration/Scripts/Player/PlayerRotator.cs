using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] 
    private InputInfo _inputInfo;
    [SerializeField] 
    private Rigidbody _rb;

    
    [SerializeField][Range(0,10)]
    private float _turnVelocity;

    private float _rotationY;
    

    private void Update()
    {
        _rotationY = _rb.rotation.eulerAngles.y;

        Vector3 inputDir = _inputInfo.GetMovementInfo();
        if (inputDir.sqrMagnitude > Mathf.Epsilon)
            _rotationY = Quaternion.LookRotation(inputDir, Vector3.up).eulerAngles.y;
    }

    private void FixedUpdate()
    {
        var rotationDif = Mathf.DeltaAngle(_rb.rotation.eulerAngles.y, _rotationY);
        //поворот как можно меньше
        var velocity = Mathf.Min(Mathf.Abs(rotationDif), _turnVelocity);
        //плавное вращение
        _rb.angularVelocity = Vector3.up * velocity * Mathf.Sign(rotationDif);
    }
}
