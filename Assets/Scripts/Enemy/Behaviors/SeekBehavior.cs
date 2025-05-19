using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekBehavior : SteeringBehavior
{

    [SerializeField] private float targetReachedThreshold = 0.5f;
    [SerializeField] private bool showGizmo = true;

    private bool _reachedLastTarget = true;

    private Vector2 _targetPositionCached;
    private float[] _interestTemp;


    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {
        if (_reachedLastTarget)
        {
            if (aiData.targets == null || aiData.targets.Count <= 0)
            {
                aiData.currentTarget = null;
                return (danger, interest);
            }
            else
            {
                _reachedLastTarget = false;
                aiData.currentTarget = aiData.targets
                    .OrderBy(target => Vector2.Distance(target.position, transform.position)).First();
            }
        }

        if (aiData.currentTarget != null && aiData.targets != null && aiData.targets.Contains(aiData.currentTarget))
        {
            _targetPositionCached = aiData.currentTarget.position;
        }

        if (Vector2.Distance(transform.position, _targetPositionCached) < targetReachedThreshold)
        {
            _reachedLastTarget = true;
            aiData.currentTarget = null;
            return (danger, interest);
        }

        Vector2 directionToTarget = (_targetPositionCached - (Vector2)transform.position);
        for (int i = 0; i < interest.Length; i++)
        {
            float result = Vector2.Dot(directionToTarget.normalized,Directions.EightDirections[i]);

            if (result > 0)
            {
                float valueToPutIn = result;
                if (valueToPutIn > interest[i])
                {
                    interest[i] = valueToPutIn;
                }
            }
        }

        _interestTemp = interest;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {
        if (showGizmo == false)
        {
            return;
        }
        Gizmos.DrawSphere(_targetPositionCached, 0.2f);
        if (Application.isPlaying && _interestTemp != null)
        {
            if (_interestTemp != null)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < _interestTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.EightDirections[i] * _interestTemp[i]);
                }

                if (_reachedLastTarget == false)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(_targetPositionCached, 0.1f);
                }
            }
        }
    }
}
