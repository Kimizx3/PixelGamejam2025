using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public SO_GoldType gold;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.PlayerInventory.AddGold(gold.goldAmount);
            Destroy(gameObject);
        }
    }
}
