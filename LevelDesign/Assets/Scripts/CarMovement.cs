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
    public float lightThreshold = 0.0f;

    // Private variables
    [Header("Lists")]
    public GameObject[] pointsToGo;
    public GameObject[] trafficLights;

    private int index = 0;
    private Vector3 currentGoalPos = Vector3.zero;
    private bool arrived = false;

    private float lastTimeStopped = 0.0f;

    private void Start()
    {
        currentGoalPos = pointsToGo[index].transform.position; 
        lastTimeStopped = Time.time + 3.0f;
    }

    private void Update()
    {       
        if (!arrived && Time.time > lastTimeStopped + trafficLightTime)
        {
            Vector3 directionVec = currentGoalPos - transform.position;
            transform.position += directionVec.normalized * speed * Time.deltaTime;

            // Stop at traffic light
            foreach (GameObject g in trafficLights)
            {
                Vector3 distance = g.transform.position - transform.position;
                if (g.tag == "TrafficLight" && distance.magnitude < lightThreshold)
                {
                    lastTimeStopped = Time.time;
                    g.tag = "Untagged";
                }
            }

            // Change position
            if (directionVec.magnitude < threshold)
            {
                index++;

                if (index < pointsToGo.Length && !arrived)
                {
                    currentGoalPos = pointsToGo[index].transform.position;
                    gameObject.transform.LookAt(currentGoalPos);
                }
                else
                    arrived = true;

            }
        }
    }
}