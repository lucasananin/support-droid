using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Referencias;
    [SerializeField] Transform _playerTarget = null;
    private Vector3 _currentVelocity = Vector3.zero;

    // Valores;
    [SerializeField] float _distanceFromTarget = 12f;
    [SerializeField] float _smoothTime = 20f;

    // Mensagens;
    private void Start()
    {
        _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(Transform)) as Transform;
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    // Personalizados;
    private void FollowPlayer()
    {
        Vector3 _newPos = _playerTarget.position - transform.forward * _distanceFromTarget;
        transform.position = Vector3.SmoothDamp(transform.position, _newPos, ref _currentVelocity, _smoothTime * Time.deltaTime);
    }
}
