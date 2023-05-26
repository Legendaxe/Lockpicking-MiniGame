using System;
using UnityEngine;

public class Lockpick3d : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float pickSpeed;
    [SerializeField] private float lockRotationSpeed;
    [SerializeField] private float lockOpenRange;

    private float _pickRotation = 0.5f;
    private float _pickPosition;
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

    public float PickPosition
    {
        get => _pickPosition;
        set
        {
            _pickPosition = value;
            _pickPosition = Mathf.Clamp(_pickPosition, 0, 1);
        }
    }

    private void Start()
    {
        _pickOpenRotation = UnityEngine.Random.value;
    }
    

    private void Update()
    {
        HandleInput();
        
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("LockPosition", _pickRotation);
        animator.SetFloat("LockOpen", _pickPosition);
    }

    private void HandleInput()
    {
        PickRotation += Input.GetAxis("Horizontal") * pickSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            PickPosition += lockRotationSpeed * Time.deltaTime;
        }
        else
        {
            _pickPosition -= lockRotationSpeed * Time.deltaTime;
        }
    }
}
