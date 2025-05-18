using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //[SerializeField] private Animator animator;
    [Header("Input Reference")]
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference attack;
    [SerializeField] private InputActionReference pointerPosition;
    
    private Vector2 _movementInput;
    private Vector2 _pointerInput;
    private bool _skillInput;
    private bool _attackInput;
    private AgentMover _playerMovement;
    private WeaponParent _weaponParent;
    private Camera _cam;
    //private WeaponParent weaponParent;
    
    
    private void Awake()
    {
        //_cam = Camera.main;
        _playerMovement = GetComponent<AgentMover>();
        _weaponParent = GetComponentInChildren<WeaponParent>();
    }

    private void Update()
    {
        _pointerInput = GetPointerInput();
        //Debug.Log($"Pointer World Position: {_pointerInput}");
        _weaponParent.PointerPosition = _pointerInput;
        HandleInput();
        _playerMovement.MovementInput = _movementInput;
    }
    

    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
    }

    private void OnDisable()
    {
        attack.action.canceled -= PerformAttack;
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        if (_weaponParent == null)
        {
            //Debug.LogError("Weapon parent is null");
            return;
        }
        _weaponParent.PositioningWeapon();
        _weaponParent.Fire();
    }

    private void HandleInput()
    {
        _movementInput = movement.action.ReadValue<Vector2>();
    }

    // private void HandleAnimation()
    // {
    //     if (animator)
    //     {
    //         animator.SetFloat("Horizontal", _movementInput.x);
    //         animator.SetFloat("Vertical", _movementInput.y);
    //         animator.SetFloat("Speed", _movementInput.sqrMagnitude);
    //     }
    // }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        Vector3 worldPos =
            Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y,
                Mathf.Abs(Camera.main.transform.position.z)));
        return new Vector2(worldPos.x, worldPos.y);
    }
}
