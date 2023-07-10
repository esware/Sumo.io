using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;

public class Sugar : MonoBehaviour
{
    public float rotationDuration = 2f;
    public Vector3 rotationAxis = Vector3.up;

    private void Start()
    {
        transform.rotation =Quaternion.Euler(90,0,0); 
        StartRotation();
    }
    

    private void StartRotation()
    {
        transform.DORotate(transform.rotation.eulerAngles + rotationAxis * 360f, rotationDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            GameEvents.collectableEvent?.Invoke(this.gameObject);
        }
    }
}
