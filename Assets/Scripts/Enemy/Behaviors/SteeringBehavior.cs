using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehavior : MonoBehaviour
{
    public abstract (float[] danger, float[] interest)
        GetSteering(float[] danger, float[] interest, AIData aiData);
}

public static class Directions
{
    public static List<Vector2> EightDirections = new List<Vector2>()
    {
        new Vector2(0, 1).normalized,
        new Vector2(1, 1).normalized,
        new Vector2(1, 0).normalized,
        new Vector2(1, -1).normalized,
        new Vector2(0, -1).normalized,
        new Vector2(-1, -1).normalized,
        new Vector2(-1, 0).normalized,
        new Vector2(-1, 1).normalized,
    };
}
