using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Lockpick3d : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float pickSpeed = 1f;
    [SerializeField] private float lockRotationSpeed = 1f;
    [SerializeField] private float lockOpenRange = 0.5f;

    private float _pickRotation = 0.5f;
    private float _lockPosition;
    private float _pickOpenRotation;

    public float PickRotation
    {
        get => _pickRotation;
        set
        {
            _pickRotation = value;
            _pickRotation = Mathf.Clamp(_pickRotation, 0, 1);
        }
    }

    public float LockPosition
    {
        get => _lockPosition;
        set
        {
            _lockPosition = value;
            _lockPosition = Mathf.Clamp(_lockPosition, 0, MaxLockPosition);
        }
    }

    private float MaxLockPosition
        => 1 - Mathf.Clamp(MathF.Abs(_pickRotation - _pickOpenRotation) - lockOpenRange, 0, 1);

    private void Start()
    {
        InitializeNewLock();
    }
    

    private void Update()
    {
        HandleInput();

        UpdateAnimator();
    }

    private void HandleLockPosition()
    {
        if (_lockPosition >= 1f)
        {
            Debug.Log("You Win!!!");
        }
    }

    private void InitializeNewLock()
    {
        _pickOpenRotation = UnityEngine.Random.value;
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("LockPosition", _pickRotation);
        animator.SetFloat("LockOpen", _lockPosition);
    }

    private void HandleInput()
    {
        PickRotation += Input.GetAxis("Horizontal") * pickSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            LockPosition += lockRotationSpeed * Time.deltaTime;
            HandleLockPosition();
        }
        else
        {
            _lockPosition -= lockRotationSpeed * Time.deltaTime;
        }
    }
}
