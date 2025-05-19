using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseEnemy
{
    void ChangeState(AIState newState);
    AIState GetCurrentState();

    void DisableCollision(CapsuleCollider2D collision);
    void EnableCollision(CapsuleCollider2D collision);
    Rigidbody2D GetRigidBody();

    void PerformAttack();

    void Initialize(Transform playerTransform);
}

public enum AIState
{
    Chase,
    Attack,
    Die
}
