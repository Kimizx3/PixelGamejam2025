using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public Color baseColor, offsetColor;
    public SpriteRenderer spriteRenderer;
    public GameObject highlight;

    public void Init(bool isOffset)
    {
        spriteRenderer.color = isOffset ? baseColor : offsetColor;
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
