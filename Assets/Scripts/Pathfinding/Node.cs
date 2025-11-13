using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{
    public Vector3 worldPosition;
    public Vector2Int gridIndex;
    public bool isObstacle;
    public Node parent;
    public List<Node> neighbors;
    public float cost;

    public Node(Vector3 worldPosition, Vector2Int gridIndex, bool isObstacle, float cost)
    {
        this.worldPosition = worldPosition;
        this.gridIndex = gridIndex;
        this.isObstacle = isObstacle;
        this.cost = cost;
        parent = null;
        neighbors = new List<Node>();
    }
}
