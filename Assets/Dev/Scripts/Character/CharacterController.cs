using System;
using Dev.Scripts;
using DG.Tweening;
using Managers;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private CharacterMovementController _movementController;
    private CharacterAnimationController _animationController;

    private Joystick _joystick;
    private Rigidbody _rb;
    private readonly float sizeIncreaseAmount = 0.1f;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _joystick = FindObjectOfType<Joystick>();
        _movementController = GetComponent<CharacterMovementController>();
        _animationController = GetComponent<CharacterAnimationController>();
    }

    private void OnDestroy()
    {
        GameEvents.loseEvent?.Invoke();
    }

    private void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        Vector3 moveDirection = GetMoveDirection();
        _movementController.Move(moveDirection);
        _animationController.PlayAnimation(moveDirection);
    }

    private Vector3 GetMoveDirection()
    {
        float horizontalInput = _joystick.Horizontal;
        float verticalInput = _joystick.Vertical;
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        return moveDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                float forceMagnitude = 3f * transform.localScale.magnitude / collision.gameObject.transform.localScale.magnitude;

                Vector3 forceDirection = otherRigidbody.transform.position - transform.position;
                forceDirection.y = 0f;
                forceDirection.Normalize();
                Vector3 appliedForce = forceDirection * forceMagnitude;

                otherRigidbody.DOMove(otherRigidbody.transform.position + forceDirection * 5f, .2f).SetEase(Ease.InSine);
            }
        }
    }
    
    private void EatSugar()
    {
        transform.localScale += new Vector3(sizeIncreaseAmount, sizeIncreaseAmount, sizeIncreaseAmount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Candy"))
        {
            EatSugar();
        }
    }
}
