using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryBinder : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance?.BindVictoryUI(gameObject);
    }
}
