using UnityEngine;

namespace Dev.Scripts.Enemy
{
    public class EnemyGravityApplier : MonoBehaviour, IGravityApplier
    {
        public float gravity = 9.8f;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void ApplyGravity()
        {
            _rb.AddForce(-Vector3.up * gravity);
        }
    }
}