using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WeaponUpgradeUI : MonoBehaviour
{
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private Transform buttonContainer;
    public WeaponUpgrade weapon;

    private List<UpgradeMenu> _upgradeMenus = new List<UpgradeMenu>();

    private void Awake()
    {
        CloseUpgradeUI();
        _upgradeMenus.Add(new UpgradeMenu("Increase Damage", () => weapon.UpgradeDamage(5)));
        _upgradeMenus.Add(new UpgradeMenu("Boost Fire Rate", () => weapon.UpgradeFireRate(1.35f)));
        _upgradeMenus.Add(new UpgradeMenu("Increase Bullet Size", () => weapon.UpgradeBulletSize(2)));
        _upgradeMenus.Add(new UpgradeMenu("Decrease Reload Time", () => weapon.UpgradeReloadTime(0.8f)));
        _upgradeMenus.Add(new UpgradeMenu("Add Projectile Count", () => weapon.UpgradeProjectiles(2)));
        
        Debug.Log($"[Upgrade UI] Total Upgrades Available: {_upgradeMenus.Count}");
    }

    private void Start()
    {
        
    }

    public void ShowUpgradeOptions()
    {
        // Pause the game
        Time.timeScale = 0;
        
        // Clear old buttons
        foreach (Transform button in buttonContainer)
        {
            Destroy(button.gameObject);
        }
        
        // Shuffle and pick 3
        _upgradeMenus.Sort((a, b) => Random.value > 0.5f ? 1 : -1);
        
        
        // Ensure the number
        var options = _upgradeMenus.GetRange(0, 3);
        
        Debug.Log($"Showing {options} upgrade options");

        foreach (var option in options)
        {
            var button = Instantiate(buttonPrefab, buttonContainer);

            var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = option.upgradeName;
            }
            
            button.onClick.AddListener(() =>
            {
                option.ApplyUpgrade();
                Debug.Log($"Upgrade applied: {option.upgradeName}");
                CloseUpgradeUI();
            });
        }
        
        gameObject.SetActive(true);
    }

    private void CloseUpgradeUI()
    {
        gameObject.SetActive(false);

        Time.timeScale = 1;
    }

}
