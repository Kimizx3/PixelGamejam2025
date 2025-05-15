using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    
    private void Update()
    {
        goldText.text = $"Gold: {Inventory.PlayerInventory.GetGold()}";
    }
}
