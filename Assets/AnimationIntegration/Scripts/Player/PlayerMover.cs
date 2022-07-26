using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] 
    private InputInfo _inputInfo;
    [SerializeField] 
    private Rigidbody _rb;
 
    [Header("Настройка:")]
    [SerializeField][Min(0)]
    private float _acceleration; 
    [SerializeField] 
    private ScriptableValueField<float> _maxVelocity;
    
    private Vector3 _targetVelocity;


    private void Update()
    {
        _targetVelocity = _inputInfo.GetMovementInfo() * _maxVelocity.Value;
    }

    

    private void FixedUpdate()
    {
        //движение с ускорением
        var curVelocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        var dif = _targetVelocity - curVelocity;
        var acceleration = Mathf.Min(dif.magnitude / Time.deltaTime, _acceleration);
        
        _rb.AddForce(dif.normalized * acceleration, ForceMode.Acceleration);
    }
}
