using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomWalkParameters_", menuName = "PCG/RandomWalkGen_Data")]
public class SO_RandomWalkGenerator : ScriptableObject
{
    public int iteration = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;
}
