using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyAI : MonoBehaviour, ITargetProvider
{
    public float detectionRadius = 5f;
    public LayerMask candyLayer;
    public LayerMask enemyLayer;

    private Transform _target=null;

    public Transform GetTarget()
    {
        _target = FindNearestCandy();
        
        if (_target == null)
        {
            _target = FindNearestEnemy();
        }
        return _target;
    }

    private Transform FindNearestCandy()
    {
        Collider[] candies = Physics.OverlapSphere(transform.position, detectionRadius, candyLayer);

        Transform nearestCandy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider candy in candies)
        {
            float distance = Vector3.Distance(transform.position, candy.transform.position);
            if (distance < nearestDistance)
            {
                nearestCandy = candy.transform;
                nearestDistance = distance;
            }
        }

        if (nearestCandy == null)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

            foreach (Collider enemy in enemies)
            {
                nearestCandy = enemy.transform;
                break;
            }
        }

        return nearestCandy;
    }

    private Transform FindNearestEnemy()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider enemyCollider in enemyColliders)
        {
            if (enemyCollider.transform != transform)
            {
                float distance = Vector3.Distance(transform.position, enemyCollider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestEnemy = enemyCollider.transform;
                    nearestDistance = distance;
                }
            }
        }

        return nearestEnemy;
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
                var targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                    targetRigidbody.DOMove(targetRigidbody.transform.position + forceDirection * 3f, .2f).SetEase(Ease.InSine);
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