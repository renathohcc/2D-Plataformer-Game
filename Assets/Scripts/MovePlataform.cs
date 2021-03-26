using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlataform : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed;
    int currentPointIndex;

    float waitTime;
    public float startWaitTime;

    void Start()
    {
        //To input the position settings from the points to the plataform object
        transform.position = patrolPoints[0].position;
        //Time to wait until the patrol starts
        waitTime = startWaitTime;
        
    }

    // Update is called once per frame
    void Update()
    {
       //To make the plataform move from one start position to and end position, with some speed 
       transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

       //To choice whats happen when the plataform position is equal a point position
       if(transform.position == patrolPoints[currentPointIndex].position)
        {
            if(waitTime <= 0)
            {
                if(currentPointIndex + 1 < patrolPoints.Length)
                {
                    currentPointIndex++;
                }else{
                    currentPointIndex = 0;
            }
                waitTime = startWaitTime;
            }else{
                waitTime -= Time.deltaTime;
            }   
        }
    }

    //Functions to make the player move together to the plataform
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            collision.transform.parent = transform;
        }
    }

     private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            collision.transform.parent = null;
        }
    }
}
