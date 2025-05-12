using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePose;

    private void Awake()
    {
        movePose.parent = null;
    }

    private void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePose.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePose.position) <= 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                movePose.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                movePose.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }
        }
    }
}
