using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : Obstacle
{
    [SerializeField] private float movSpeed = 1;
    private float positionT = 0;
    private float laneDistanceX = 2;

    void Update()
    {
        positionT += Time.deltaTime * movSpeed;
        float lanePositionX = (Mathf.PingPong(positionT, 1) - 0.5f) * laneDistanceX * 2;

        Vector3 pos = transform.position;
        pos.x = lanePositionX;
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider collider)
    {
        movSpeed = 0;
    }
}