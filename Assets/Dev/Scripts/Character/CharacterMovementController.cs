using UnityEngine;

namespace Dev.Scripts
{
    public interface ICharacterMovementController
    {
        void Move(Vector3 moveDirection);
    }

    public class CharacterMovementController : MonoBehaviour, ICharacterMovementController
    {
        private Rigidbody _rigidbody;
        public int rotationSpeed = 5;
        public float moveSpeed = 5f;
        public float gravity = 9.81f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 moveDirection)
        {
            ApplyGravity();
            if (_rigidbody.velocity.y < -1)
                return;

            Vector3 velocity = moveDirection * moveSpeed;
            velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;

            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
        
        private void ApplyGravity()
        {
            Vector3 gravityForce = Vector3.down * (gravity * _rigidbody.mass);
            _rigidbody.AddForce(gravityForce, ForceMode.Acceleration);
        }
    }
}
