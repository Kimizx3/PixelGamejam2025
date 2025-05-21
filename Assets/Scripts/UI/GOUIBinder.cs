using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOUIBinder : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance?.BindGameOverUI(gameObject);
    }
}
