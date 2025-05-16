using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private AgentMover _agentMover;

    private WeaponParent _weaponParent;
    private Vector2 pointerInput, movementInput;
    
    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    private void Update()
    {
        _agentMover.MovementInput = movementInput;
        //_weaponParent.PointerPosition = pointerInput;
    }

    public void PerformAttack()
    {
        _weaponParent.Attack();
    }

    private void Awake()
    {
        _weaponParent = GetComponentInChildren<WeaponParent>();
        _agentMover = GetComponent<AgentMover>();
    }
}
