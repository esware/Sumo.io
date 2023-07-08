using UnityEngine;

namespace Dev.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private ITargetProvider _targetProvider;
        private IMovementController _movementController;
        private IGravityApplier _gravityApplier;

        private void Awake()
        {
            _targetProvider = GetComponent<ITargetProvider>();
            _movementController = GetComponent<IMovementController>();
            _gravityApplier = GetComponent<IGravityApplier>();
        }

        private void Update()
        {
            Transform target = _targetProvider.GetTarget();
            _movementController.MoveTowards(target);
            _gravityApplier.ApplyGravity();
        }
    }
}
