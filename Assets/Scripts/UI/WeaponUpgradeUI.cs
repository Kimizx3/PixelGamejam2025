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
    [SerializeField] private WeaponUpgrade weapon;

    private List<UpgradeMenu> _upgradeMenus = new List<UpgradeMenu>();

    private void Start()
    {
        _upgradeMenus.Add(new UpgradeMenu("Increase Damage", () => weapon.UpgradeDamage(5)));
        _upgradeMenus.Add(new UpgradeMenu("Boost Fire Rate", () => weapon.UpgradeDamage(5)));
        _upgradeMenus.Add(new UpgradeMenu("Increase Bullet Size", () => weapon.UpgradeDamage(5)));
        _upgradeMenus.Add(new UpgradeMenu("Decrease Reload Time", () => weapon.UpgradeDamage(5)));
        _upgradeMenus.Add(new UpgradeMenu("Add Projectile Count", () => weapon.UpgradeDamage(5)));
    }

    public void ShowUpgradeOptions()
    {
        foreach (Transform button in buttonContainer) Destroy(button.gameObject);
        
        // Shuffle and pick 3
        _upgradeMenus.Sort((a, b) => Random.value > 0.5f ? 1 : -1);
        var options = _upgradeMenus.GetRange(0, Mathf.Min(3, _upgradeMenus.Count));

        foreach (var option in options)
        {
            var button = Instantiate(buttonPrefab, buttonContainer);
            button.GetComponentInChildren<TextMeshProUGUI>().text = option.upgradeName;
            button.onClick.AddListener(() =>
            {
                option.ApplyUpgrade();
                CloseUpgradeUI();
            });
        }
    }

    private void CloseUpgradeUI()
    {
        gameObject.SetActive(false);
    }

}
