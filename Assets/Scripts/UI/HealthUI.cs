using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    private List<GameObject> hearts = new List<GameObject>();

    public void InitializeHearts(int maxHealth)
    {
        ClearHearts();

        int difference = maxHealth - hearts.Count;

        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                GameObject heart = Instantiate(heartPrefab, transform);
                hearts.Add(heart);
            }
        }
        
        for (int i = 0; i < maxHealth; i++)
        {
            
            hearts[i].SetActive(i < maxHealth);
        }
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].SetActive(i < currentHealth);
            StartCoroutine(PluseHeart(hearts[i]));
        }
    }

    private void ClearHearts()
    {
        foreach (var heart in hearts)
        {
            Destroy(heart);
        }
        hearts.Clear();
    }

    IEnumerator PluseHeart(GameObject heart)
    {
        Vector3 originalScale = heart.transform.localScale;
        heart.transform.localScale = originalScale * 1.2f;
        yield return new WaitForSeconds(0.1f);
        heart.transform.localScale = originalScale;
    }
}
