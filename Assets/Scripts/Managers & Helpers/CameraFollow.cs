using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed = 5f;
    private Transform player;
    private void Update()
    {
        if (player == null) player = PlayerLocator.Player.transform;
        Vector3 newPos = new Vector3(player.position.x, player.position.y, -10);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
