using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyController : MonoBehaviour
{
    public static Action EnemyDeliveredPackage;

    private GizmoManager grid;
    private Node startNode;
    private Node goalNode;
    private List<Node> path;
    private int currentIndex = 0;
    private float speed = 3f;
    private Vector3 targetPosition;
    private AStar astar;

    private void OnEnable()
    {
        EnemySpawner.TranslateGoalCoords += GoalSpawned;
        Driver.PackageEffectsDisable += OnReachedTarget;
    }

    private void GoalSpawned(Vector3 position)
    {
        targetPosition = position;
        Debug.Log(targetPosition);
    }

    private void Start()
    {
        astar = GetComponent<AStar>();
        goalNode = GizmoLocator.GizmoManager.GetNodeFromWorldPosition(targetPosition);
        CalculatePath();
    }

    private void CalculatePath()
    {
        path = new List<Node>();
        Node startNode = GizmoLocator.GizmoManager.GetNodeFromWorldPosition(transform.position);
        path = astar.SearchPath(startNode, goalNode);

        if (path == null || path.Count == 0)
        {
            Debug.LogError($"Path is null");
            //Destroy(gameObject);
            return;
        }

        currentIndex = 0;
    }

    private void Update()
    {
        if (path == null) return;
        if (currentIndex >= path.Count) return;

        Vector3 target = path[currentIndex].worldPosition;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target) <  .1f)
        {
            currentIndex++;

            if(currentIndex >= path.Count)
            {
                EnemyDeliveredPackage?.Invoke();
                OnReachedTarget();
            }
        }
    }

    private void OnReachedTarget()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        EnemySpawner.TranslateGoalCoords -= GoalSpawned;
        Driver.PackageEffectsDisable -= OnReachedTarget;
    }
}
