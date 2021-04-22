using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public float maxTime = 50.0f;
    public float threshold = 5.0f;
    
    private GameObject player;
    private Vector3 goalPos;
    private bool arrived = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        goalPos = GameObject.Find("CharacterObjective").transform.position;
    }

    void Update()
    {
        Vector3 directionVec = goalPos - player.transform.position;
        float substraction = maxTime - Time.time;

        if (substraction <= 0 && !arrived)
            text.text = "You lost the car!";
        else if (directionVec.magnitude <= threshold)
        {
            text.text = "You got them!";
            arrived = true;
        }
        else
            text.text = "00:" + substraction.ToString("F1");
    }
}
