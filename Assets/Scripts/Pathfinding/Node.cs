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

    public float gCost; //реальная стоимость от старта до текущей ноды
    public float hCost; //эвристика
    public float fCost { get { return hCost + gCost; } }

    public Node(Vector3 worldPosition, Vector2Int gridIndex, bool isObstacle, float cost)
    {
        this.worldPosition = worldPosition;
        this.gridIndex = gridIndex;
        this.isObstacle = isObstacle;
        this.cost = cost;

        parent = null;
        neighbors = new List<Node>();

        gCost = float.MaxValue;
        hCost = 0;
    }
}
