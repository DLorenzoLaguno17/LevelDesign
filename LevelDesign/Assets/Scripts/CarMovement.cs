using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [Header("General")]
    public float threshold = 0.0f;
    public float speed = 20.0f;

    [Header("Traffic Lights")]
    public float trafficLightTime = 2.0f;

    // Private variables
    private int index = 0;
    private GameObject[] pointsToGo;
    private Vector3 currentGoalPos = Vector3.zero;
    private bool arrived = false;

    private float lastTimeStopped = 0.0f;

    private void Start()
    {
        pointsToGo = GameObject.FindGameObjectsWithTag("PointToGo");
        currentGoalPos = pointsToGo[index].transform.position;
    }

    private void Update()
    {
        if (!arrived)
        {
            Vector3 directionVec = currentGoalPos - transform.position;

            transform.position += directionVec.normalized * speed * Time.deltaTime;

            if (directionVec.magnitude < threshold)
            {
                index++;

                if (index < pointsToGo.Length)
                    currentGoalPos = pointsToGo[index].transform.position;
                else
                    arrived = true;

                transform.LookAt(currentGoalPos);
            }
        }
    }
}