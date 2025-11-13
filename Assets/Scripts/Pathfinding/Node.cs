using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{
    public Vector3 position;
    public Vector2Int worldPosition;
    public bool isObstacle;
    public Node parent;
    public List<Node> neighbors;
    public float cost;

    public Node(Vector3 position, Vector2Int gridIndex, bool isObstacle, float cost)
    {
        this.position = position;
        worldPosition = gridIndex;
        this.isObstacle = isObstacle;
        this.cost = cost;
        parent = null;
        neighbors = new List<Node>();
    }
}
