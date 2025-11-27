using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AStar : MonoBehaviour
{
    private PriorityQueue<Node> queue;

    public List<Node> SearchPath(Node startNode, Node goalNode)
    {
        if (startNode == null || goalNode == null)
        {
            Debug.LogWarning("Goal node or stat Node is null");
            return null;
        }

        if(startNode == goalNode)
        {
            Debug.Log("Start Node == Goal Node");
            return new List<Node> { startNode };
        }
        GizmoLocator.GizmoManager.ResetAllNodes();
        queue = new PriorityQueue<Node>();

        startNode.gCost = 0; // стоимость от старта до текущей клетки
        startNode.hCost = GetHeuristic(startNode, goalNode); //эвристика
        startNode.parent = null;
        queue.Enqueue(startNode, startNode.fCost); // 0 + hCost

        while(queue.Count > 0)
        {
            Node current = queue.Dequeue();

            if(current == goalNode)
                return CreatePath(current);

            if (current.isObstacle) continue;

            foreach (Node neighbor in current.neighbors)
            {
                if (neighbor.isObstacle) continue;

                float movementCost = GetMovementCost(current, neighbor); //Стоимость перехода с текущей к соседу (диагональ или нет)
                float tempGCost = current.gCost + movementCost; // новая стоимость от старта до текущей ноды

                if(tempGCost < neighbor.gCost) //ищем минимальную стоимость 
                {
                    neighbor.gCost = tempGCost; //обновляем минимальную стоимость
                    neighbor.hCost = GetHeuristic(neighbor, goalNode); //получаем эвристику от соседа до целевой ноды
                    neighbor.parent = current;

                    queue.Enqueue(neighbor, neighbor.fCost);
                }
            }
        }

        //Debug.LogWarning("Path not found");
        return null;
    }

    private List<Node> CreatePath(Node current)
    {
        List<Node> path = new List<Node>();
        Node temp = current;
        while(temp != null)
        {
            path.Add(temp);
            temp = temp.parent;
        }

        path.Reverse();
        Debug.Log($"Количество шагов: {path.Count}");
        return path;
    }

    private float GetHeuristic(Node from, Node to)
    {
        int dx = Mathf.Abs(from.gridIndex.x - to.gridIndex.x);
        int dy = Mathf.Abs(from.gridIndex.y - to.gridIndex.y);

        float straight = 1.0f;
        float diagonal = 1.414f;

        return straight * (dx + dy) + (diagonal - 2 * straight) * MathF.Min(dx, dy);
    }

    private float GetMovementCost(Node from, Node to)
    {
        int dx = Mathf.Abs(from.gridIndex.x - to.gridIndex.x);
        int dy = Mathf.Abs(from.gridIndex.y - to.gridIndex.y);

        bool isDiagonal = (dx == 1 && dy == 1);

        if (isDiagonal)
            return to.cost * 1.414f;
        else
            return to.cost * 1.0f;
    }
}
