using UnityEngine;

public class EnemyMovementController : MonoBehaviour, IMovementController
{
    public float moveSpeed = 3f;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void MoveTowards(Transform target)
    {
        if (target == null)
            return;
        
        var direction = target.position - transform.position;
        direction.y = 0f;
        direction.Normalize();
        _rb.velocity = new Vector3(direction.x * moveSpeed, _rb.velocity.y, direction.z * moveSpeed);
    }
}