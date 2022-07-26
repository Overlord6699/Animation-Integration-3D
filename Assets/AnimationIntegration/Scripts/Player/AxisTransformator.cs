using UnityEngine;

public class AxisTransformator : MonoBehaviour
{
    [SerializeField]
    private InputInfo _inputController;

    private void Awake()
    {
        Vector3 rightVec = Vector3.ProjectOnPlane(transform.right, Vector3.up);
        Vector3 forwardVec = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        
        _inputController.SetCorrectedDirections(rightVec, forwardVec);
    }
}
