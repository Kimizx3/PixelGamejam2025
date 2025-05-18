using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] private List<SteeringBehavior> steeringBehaviors;
    
    [SerializeField] private List<Detector> detectors;
    [SerializeField] private AIData aiData;
    [SerializeField] private float detectionDelay = 0.05f, aiUpdateDelay = 0.06f, attackDelay = 1f;

    [SerializeField] private float attackDistance = 1f;

    public UnityEvent onAttackPressed;
    public UnityEvent<Vector2> onMovementInput, onPointerInput;

    [SerializeField] private Vector2 movementInput;
    [SerializeField] private ContextSolver movementDirectionSolver;
    private bool _following = false;
    
    public SO_EnemyType _enemyType;
    public SO_GoldType goldData;

    public int heath = 4;

    

    private void Start()
    {
        InvokeRepeating("PerformDetection", 0, detectionDelay);
    }
    

    public void TakeDamage(int damage)
    {
        heath -= damage;

        if (heath <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        int goldAmount = _enemyType.exp;
        for (int i = 0; i < goldAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Instantiate(goldData.basicGoldPrefab, 
                transform.position + (Vector3)randomOffset, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }

    private void Update()
    {
        if (aiData.currentTarget != null)
        {
            onPointerInput?.Invoke(aiData.currentTarget.position);
            if (_following == false)
            {
                _following = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if (aiData.GetTargetsCount() > 0)
        {
            aiData.currentTarget = aiData.targets[0];
        }
        onMovementInput?.Invoke(movementInput);
    }

    private IEnumerator ChaseAndAttack()
    {
        if (aiData.currentTarget == null)
        {
            movementInput = Vector2.zero;
            _following = false;
            yield return null;
        }
        else
        {
            float distance = Vector2.Distance(aiData.currentTarget.position, 
                transform.position);

            if (distance < attackDistance)
            {
                movementInput = Vector2.zero;
                onAttackPressed?.Invoke();
                yield return new WaitForSeconds(attackDelay);
                StartCoroutine(ChaseAndAttack());
            }
            else
            {
                movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviors,
                    aiData);
                yield return new WaitForSeconds(aiUpdateDelay);
                StartCoroutine(ChaseAndAttack());
            }
        }
    }
}
