using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Grid/TileCost")]
public class TileCost : ScriptableObject
{
    public float cost = 1f;
    public bool isObstacle = false;
}
