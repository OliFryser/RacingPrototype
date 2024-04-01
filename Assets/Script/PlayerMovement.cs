using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _acceleration = 10f;

    [SerializeField]
    private float _maxSpeed = 10f;

    private Rigidbody2D _rigidbody2D;
    private float _accelerationInput;
    private float _turningInput;
    [SerializeField]
    private float _turningSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyAcceleration();
    }

    private void Update()
    {
        ApplyRotation();
    }

    private void ApplyRotation()
    {
        if (_turningInput == 0)
            return;
        //transform.Rotate(new Vector3(0, 0, _turningSpeed * -_turningInput * Time.deltaTime * _rigidbody2D.velocity.magnitude));
        var impulse = _turningInput * _turningSpeed * Mathf.Deg2Rad * _rigidbody2D.inertia * _rigidbody2D.velocity.magnitude;
        _rigidbody2D.AddTorque(-impulse, ForceMode2D.Impulse);
    }

    private void ApplyAcceleration()
    {
        Debug.Log($"{_accelerationInput}");
        if (_accelerationInput == 0 || _rigidbody2D.velocity.magnitude >= _maxSpeed)
            return;
        _rigidbody2D.AddRelativeForce(_acceleration * _accelerationInput * Vector2.up);
    }

    private void OnAcceleration(InputValue input)
    {
        _accelerationInput = input.Get<float>();

    }

    private void OnTurning(InputValue input)
    {
        _turningInput = input.Get<float>();

    }
}
