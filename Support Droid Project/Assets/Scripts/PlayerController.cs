using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Referencias;
    [SerializeField] CharacterController _cController = null;
    [SerializeField] Camera _topDownCam = null;
    [SerializeField] GameObject _aimLine = null;
    [SerializeField] Vector3 _moveDir = Vector3.zero;
    [SerializeField] Animator _anim = null;

    // Valores;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _rotSpeed = 10f;
    [SerializeField] float _gravity = 20f;
    [SerializeField] float _jumpHeight = 2f;
    [SerializeField] int _maxJump = 2;
    [SerializeField] int _currentJump = 0;
    [SerializeField] bool _isAiming = false;
    private float _verticalDir = 0f;
    private int _floorMask = 0;

    // Mensagens;
    private void Start()
    {
        _cController = GetComponent(typeof(CharacterController)) as CharacterController;
        _topDownCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent(typeof(Camera)) as Camera;
        _aimLine = transform.GetChild(1).gameObject;
        _floorMask = LayerMask.GetMask("Floor");
        _aimLine.SetActive(false);
    }

    private void Update()
    {
        ChangeStance();
        Movement();

        if (_moveDir.x != 0 || _moveDir.z != 0)
        {
            _anim.SetBool("_isMoving", true);
        }
        else
        {
            _anim.SetBool("_isMoving", false);
        }
    }

    // Personalizados;
    private void Movement()
    {
        _moveDir.Set(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        _moveDir = _moveDir.normalized * _moveSpeed;

        Rotation();
        JumpAction();

        _verticalDir -= _gravity * Time.deltaTime;
        _moveDir += Vector3.up * _verticalDir;
        _cController.Move(_moveDir * Time.deltaTime);
    }

    private void Rotation()
    {
        if (!_isAiming)
        {
            if (_moveDir != Vector3.zero)
            {
                Quaternion newRot = Quaternion.LookRotation(_moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRot, _rotSpeed * Time.deltaTime);
            }
        }
        else
        {
            Ray _camRay = _topDownCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hitInfo;

            if (Physics.Raycast(_camRay, out _hitInfo, 50f, _floorMask))
            {
                Vector3 _faceDir = _hitInfo.point - transform.position;
                _faceDir.y = 0f;
                Quaternion _newRot = Quaternion.LookRotation(_faceDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, _newRot, _rotSpeed * Time.deltaTime);
            }
        }
    }

    private void JumpAction()
    {
        if (_cController.isGrounded)
        {
            _verticalDir = 0f;
            _currentJump = 0;
        }

        if (Input.GetButtonDown("Jump") && _currentJump < _maxJump)
        {
            float _jumpDir = Mathf.Sqrt(-2 * -_gravity * _jumpHeight);
            _verticalDir = _jumpDir;
            _currentJump++;
        }
    }

    private void ChangeStance()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _isAiming = !_isAiming;
            _aimLine.SetActive(_isAiming);
        }
    }
}
