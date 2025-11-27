using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float minDistanceToGoalNode = 10f;
    [SerializeField] private float minDistanceToPlayer = 10f;
    [SerializeField] private GameObject enemyPrefab;

    public static System.Action<Vector3> TranslateGoalCoords;

    private GameObject enemy;
    private Vector3 playerPos;
    private Vector3 goalPos;
    private Node startNode;
    private GizmoManager grid;
    private List<Node> allNodes;
    private void Awake()
    {
        grid = GetComponent<GizmoManager>();
    }

    private void OnEnable()
    {
        Driver.PackageEffectsEnable += SpawnEnemy;
        SpawnPoolManager.CustomerSpawned += GoalSpawned;
    }

    private void GoalSpawned(Vector3 position)
    {
        goalPos = position;
    }

    private void Update()
    {
        GetCurrentPlayerPosition();
    }

    private void GetCurrentPlayerPosition()
    {
        playerPos = PlayerLocator.Player.transform.position;
    }

    private void Start()
    {
        GetAllNodes();
    }

    private void GetAllNodes()
    {
        allNodes = grid.GetAllNodes();
    }

    private void SpawnEnemy()
    {
        while (allNodes.Count != 0)
        {
            int randomIndex = Random.Range(0, allNodes.Count);
            Node node = allNodes[randomIndex];
            if (node.isObstacle) continue;
            if (Vector3.Distance(node.worldPosition, goalPos) < minDistanceToGoalNode)
            {
                allNodes.RemoveAt(randomIndex);
                continue;
            }
            if (Vector3.Distance(node.worldPosition, playerPos) < minDistanceToPlayer)
            {
                allNodes.RemoveAt(randomIndex);
                continue;
            }
            if (allNodes.Count == 0) GetAllNodes();
            startNode = node;
            break;
        }
        enemy = Instantiate(enemyPrefab, startNode.worldPosition, Quaternion.identity);

        TranslateGoalCoords?.Invoke(goalPos);
    }

    private void OnDisable()
    {
        Driver.PackageEffectsEnable -= SpawnEnemy;
        SpawnPoolManager.CustomerSpawned -= GoalSpawned;
    }
}
