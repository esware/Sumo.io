using UnityEngine;

public interface ITargetProvider
{
    Transform GetTarget();
}

public interface IMovementController
{
    void MoveTowards(Transform target);
}

public interface IGravityApplier
{
    void ApplyGravity();
}
