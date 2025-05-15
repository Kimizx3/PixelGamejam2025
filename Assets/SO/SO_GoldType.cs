using UnityEngine;

[CreateAssetMenu(fileName = "Gold_", menuName = "Gold_Data")]
public class SO_GoldType : ScriptableObject
{
    public int goldAmount = 10;
    
    [Header("Gold Prefab Settings")]
    public GameObject basicGoldPrefab;
}
