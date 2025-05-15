using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory PlayerInventory { get; private set; }
    
    private int _goldCount;

    private void Awake()
    {
        if (PlayerInventory == null)
        {
            PlayerInventory = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        _goldCount += amount;
    }

    public int GetGold()
    {
        return _goldCount;
    }
}
