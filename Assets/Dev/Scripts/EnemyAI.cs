using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class EnemyAI : MonoBehaviour
{
    [Space,Header("Character Movement")]
    public LayerMask playerLayer; 
    public float moveSpeed = 3f;
    public float detectionRadius = 5f;
    public float minDistanceToPlayer = 2f; 
    public float collisionForce = 10f;
    public float gravity = 9.8f; 
    public float groundCheckDistance = 0.1f; 
    public LayerMask groundLayer; 
    
    private Rigidbody rb;
    private Transform _target;
    private Collider[] _playerColliders;
    private bool _isGrounded;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _target = FindNearestPlayer();
    }
    private void Update()
    {
        if (_target == null)
        {
            _target = FindNearestPlayer();
        }
        else
        {
            Vector3 direction = _target.position - transform.position;
            direction.y = 0f;
            direction.Normalize();
            ApplyGravity();

            rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
            
            float distanceToPlayer = Vector3.Distance(transform.position, _target.position);
            if (distanceToPlayer <= minDistanceToPlayer)
            {
            
            }   
        }
    }

    private void ApplyForceWithDotween(Rigidbody targetRigidbody, Vector3 forceDirection)
    {
        targetRigidbody.DOMove(targetRigidbody.transform.position + forceDirection * collisionForce, .2f).SetEase(Ease.InSine);
    }
    private Transform FindNearestPlayer()
    {
        Collider[] playerColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
    
        Transform nearestPlayer = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider playerCollider in playerColliders)
        {
            if (playerCollider.transform != transform)
            {
                float distance = Vector3.Distance(transform.position, playerCollider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestPlayer = playerCollider.transform;
                    nearestDistance = distance;
                }
            }
        }

        return nearestPlayer;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    
    private void ApplyGravity()
    {
        _isGrounded = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, groundCheckDistance, groundLayer))
        {
            _isGrounded = true;
        }

        if (!_isGrounded)
        {
            rb.AddForce(-Vector3.up * gravity);
        }
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
