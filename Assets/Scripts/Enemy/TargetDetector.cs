using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector
{
    [SerializeField] private float targetDetectionRange = 5;
    [SerializeField] private LayerMask obstaclesLayerMask, playerLayerMask;
    [SerializeField] private bool showGizmos = false;

    private List<Transform> _colliders;

    public override void Detect(AIData aiData)
    {
        Collider2D playerCollider = 
            Physics2D.OverlapCircle(transform.position, targetDetectionRange, playerLayerMask);

        if (playerCollider != null)
        {
            Vector2 direction = 
                (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit =
                Physics2D.Raycast(transform.position, direction, targetDetectionRange, obstaclesLayerMask);

            if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                _colliders = new List<Transform>() { playerCollider.transform };
            }
            else
            {
                _colliders = null;
            }
        }
        else
        {
            _colliders = null;
        }

        aiData.targets = _colliders;
    }

    private void OnDrawGizmosSelected()
    {
        if (showGizmos == false)
        {
            return;
        }
        
        Gizmos.DrawWireSphere(transform.position, targetDetectionRange);
        if (_colliders == null)
        {
            return;
        }
        Gizmos.color = Color.magenta;
        foreach (var item in _colliders)
        {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }
}
