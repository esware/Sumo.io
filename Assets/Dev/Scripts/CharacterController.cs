using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum TransitionParameters
{
    Idle,
    Walk,
    Run
}

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = 9.8f; // Yerçekimi kuvveti

    private Joystick _joystick;
    private Animator _animator;
    private Rigidbody _rb;
    private bool _isGrounded;

    private void Start()
    {
        _joystick = FindObjectOfType<Joystick>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveCharacter();
    }

    public void MoveCharacter()
    {
        float horizontalInput = -_joystick.Horizontal;
        float verticalInput = -_joystick.Vertical;

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        moveDirection.Normalize();

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            _rb.rotation = Quaternion.Lerp(_rb.rotation,toRotation,rotationSpeed*Time.deltaTime);
            //transform.DORotateQuaternion(toRotation, rotationSpeed * Time.deltaTime);
        }
        
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = _rb.velocity.y; 
        _rb.velocity = velocity;

        // Yerçekimi uygulama
        ApplyGravity();

        if (moveDirection != Vector3.zero)
        {
            //PlayAnim(TransitionParameters.Run.ToString(), 0.1f);
        }
        else
        {
           // PlayAnim(TransitionParameters.Idle.ToString(), 0.1f);
        }
    }

    public void PlayAnim(string targetAnimation, float animationTransitionSpeed)
    {
        if (_animator.IsInTransition(0)) { return; }
        _animator.CrossFade(targetAnimation, animationTransitionSpeed);
    }

    private void ApplyGravity()
    {
        RaycastHit hit;
        _isGrounded = Physics.Raycast(transform.position, -Vector3.up, out hit, 0.1f);
        
        if (!_isGrounded)
        {
            _rb.AddForce(Vector3.down * gravity);
        }
    }
    
    private void ApplyForceWithDotween(Rigidbody targetRigidbody, Vector3 forceDirection)
    {
        targetRigidbody.DOMove(targetRigidbody.transform.position + forceDirection * 5f, .2f).SetEase(Ease.InSine);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                Vector3 forceDirection = otherRigidbody.transform.position - transform.position;
                forceDirection.Normalize();
                ApplyForceWithDotween(otherRigidbody, forceDirection);
            }
        }
    }
    
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(this.gameObject);
        }
    }
}
