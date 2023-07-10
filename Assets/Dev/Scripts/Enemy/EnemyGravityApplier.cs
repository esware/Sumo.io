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
            Vector3 gravityForce = Vector3.down * (gravity * _rb.mass);
            _rb.AddForce(gravityForce, ForceMode.Acceleration);
        }
    }
}