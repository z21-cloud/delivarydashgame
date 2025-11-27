using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GizmoManager : MonoBehaviour
{
    //public static GizmoManager Instance;

    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private int nodeSize;
    [SerializeField] private LayerMask obstacle;

    private Dictionary<Vector2Int, Node> nodes;
    private Vector3 originMin;
    private Vector3 originMax;

    private void Awake()
    {
        nodes = new Dictionary<Vector2Int, Node>();

        originMin = ground.GetComponent<SpriteRenderer>().bounds.min;
        originMax = ground.GetComponent<SpriteRenderer>().bounds.max;

        int countX = Mathf.RoundToInt((originMax.x - originMin.x) / nodeSize);
        int countY = Mathf.RoundToInt((originMax.y - originMin.y) / nodeSize);

        for (int x = 0; x < countX; x++)
        {
            for (int y = 0; y < countY; y++)
            {
                Vector2Int gridIndex = new Vector2Int(x, y);

                float worldX = originMin.x + nodeSize * x + nodeSize / 2;
                float worldY = originMin.y + nodeSize * y + nodeSize / 2;

                Vector3 position = new Vector3(worldX, worldY, 0);
                Node newNode = new Node(position, gridIndex, false, 1);
                AssignCost(newNode);
                //GameObject worldNode = Instantiate(nodePrefab, newNode.worldPosition, Quaternion.identity);
                //worldNode.GetComponent<SpriteRenderer>().color = newNode.isObstacle ? Color.magenta : Color.red;
                nodes[gridIndex] = newNode;
            }
        }
        Debug.Log(nodes.Count);
        foreach (Node node in nodes.Values)
        {
            AddNeighbors(node);
        }
    }

    private void AssignCost(Node newNode)
    {
        Collider2D col = Physics2D.OverlapCircle(newNode.worldPosition, nodeSize / 2, obstacle);

        if (col == null) return;

        TileCostProvider provider = col.GetComponent<TileCostProvider>();

        if(provider != null)
        {
            newNode.cost = provider.tileCost.cost;
            newNode.isObstacle = provider.tileCost.isObstacle;
        }
    }

    private void AddNeighbors(Node node)
    {
        Vector2Int[] direcions =
        {
            Vector2Int.up,             // (0, 1)
            Vector2Int.down,           // (0, -1)
            Vector2Int.left,           // (-1, 0)
            Vector2Int.right,          // (1, 0)
            new Vector2Int(1, 1),      // диагональ вверх-вправо
            new Vector2Int(1, -1),     // диагональ вниз-вправо
            new Vector2Int(-1, 1),     // диагональ вверх-влево
            new Vector2Int(-1, -1)     // диагональ вниз-влево
        };

        foreach (Vector2Int direction in direcions)
        {
            Vector2Int index = node.gridIndex + direction;
            if (nodes.ContainsKey(index))
                node.neighbors.Add(nodes[index]);
        }
    }

    public Node GetNodeFromWorldPosition(Vector3 pos)
    {
        int tempX = Mathf.FloorToInt((pos.x - originMin.x) / nodeSize);
        int tempY = Mathf.FloorToInt((pos.y - originMin.y) / nodeSize);
        Vector2Int result = new Vector2Int(tempX, tempY);
        if (nodes.ContainsKey(result)) return nodes[result];
        Debug.LogWarning("No node found!");
        return null;
    }

    public void ResetAllNodes()
    {
        if (nodes.Count == 0)
            Debug.LogError("No nodes");

        foreach (Node node in nodes.Values)
        {
            node.parent = null;
            node.gCost = float.MaxValue;
            node.hCost = 0;
        }
    }

    public List<Node> GetAllNodes()
    {
        if (nodes == null)
            Debug.LogWarning("Nodes are null!!");

        List<Node> returnNodes = new List<Node>();
        foreach (Node node in nodes.Values)
        {
            returnNodes.Add(node);
        }

        return returnNodes;
    }


    /*private void OnDrawGizmos()
    {
        if (nodes == null) return;
        foreach (Node node in nodes.Values)
        {
            Gizmos.color = node.isObstacle ? Color.magenta : Color.red;
            Gizmos.DrawCube(node.worldPosition, new Vector3(nodeSize / 2, nodeSize / 2));
        }
    }*/
}
