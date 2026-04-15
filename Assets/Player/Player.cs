using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private Coroutine _powerupCoroutine;
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;


    [SerializeField] private float _speed;
    [SerializeField] private Transform _camera;
    [SerializeField]
    private float _powerupDuration;


    public void PickPowerUp()
    {
        if (_powerupCoroutine != null)
        {
            StopCoroutine(_powerupCoroutine);
        }
        _powerupCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        if(OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }

        yield return new WaitForSeconds(_powerupDuration);
        
        if(OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }
    }


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontalInput * _camera.right;
        Vector3 verticalDirection = verticalInput * _camera.forward;

        verticalDirection.y = 0;
        horizontalDirection.y = 0;

        Vector3 movementDirection = horizontalDirection + verticalDirection;
        _rigidbody.velocity = movementDirection * _speed * Time.fixedDeltaTime;
    }
}
