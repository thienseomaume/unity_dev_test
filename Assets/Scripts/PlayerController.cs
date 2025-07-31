using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _radiusCheckBall;
    [SerializeField] LayerMask _ballLayer;
    Rigidbody _rb;
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
        CheckNearBall();
    }
    private void MoveControl()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        _rb.velocity = direction.normalized * _speed;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction.normalized);
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
    }
    private void CheckNearBall()
    {
        if (Physics.CheckSphere(transform.position, _radiusCheckBall, _ballLayer))
        {
            EventCenter.Instance().OnNearBall(true);
        }
        else
        {
            EventCenter.Instance().OnNearBall(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, _radiusCheckBall);
    }
}
