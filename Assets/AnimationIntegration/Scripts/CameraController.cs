using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Vector3 _cameraPosition;


    private void LateUpdate()
    {
        var needPos = _player.position + _cameraPosition;
        transform.position = Vector3.Lerp(transform.position, needPos, _speed * Time.deltaTime);
    }


}
