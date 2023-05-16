using System;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class Lock : MonoBehaviour
{
    private Single _value;
    private Single _openValue;
    public Single Value
    {
        get => _value;
        set
        {
            if (value > maxValue || value < minValue)
                throw new ArgumentOutOfRangeException();

            _value = value;
        }
    }

    [SerializeField] private Single openValueInfelicity = 0.2f;
    [SerializeField] private Single minValue = 30;
    [SerializeField] private Single maxValue = 100;
    [SerializeField] private Single valueStep = 0.1f;
    [SerializeField] private Transform lockpickTransform;

    private void Start()
    {
        _openValue = Random.Range(minValue, maxValue);
        
        _value = (maxValue + minValue) / 2;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (_value + valueStep <= maxValue)
            {
                _value += valueStep;
                lockpickTransform.Rotate(Vector3.forward, valueStep);
            }
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            if (_value - valueStep >= minValue)
            {
                _value -= valueStep;
                lockpickTransform.Rotate(Vector3.forward, -valueStep);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryOpen();
        }
    }

    private void TryOpen()
    {
        if (Math.Abs(_value - _openValue) <= openValueInfelicity)
        {
            Debug.Log("You win");
        }
    }
}
