using UnityEngine;

public class MouseRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private Camera _mainCamera;
    [SerializeField]
    private Transform _rigidbody;
    private Vector2 _mousePosition;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        var cameraRay = _mainCamera.ScreenPointToRay(_mousePosition);
        var up = _rigidbody.rotation * Vector3.up;
        var rigidbodyPosition = _rigidbody.position;
        var mechPlane = new Plane(up, rigidbodyPosition);

        if (mechPlane.Raycast(cameraRay, out var e))
        {
            Debug.Log("sdsfgd");
            var targetPoint = cameraRay.GetPoint(e);
            var directionToTarget = targetPoint - rigidbodyPosition;

            // Rotation
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation,
                Quaternion.LookRotation(directionToTarget, up),
                _rotationSpeed * Time.fixedDeltaTime);
        }
    }
}