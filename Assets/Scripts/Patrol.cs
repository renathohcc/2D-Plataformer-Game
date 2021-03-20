using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] patrolPoints;
    public float speed;
    int currentPointIndex;

    float waitTime;
    public float startWaitTime;

    Animator anim;

    public int damage;

    void Start()
    {
        //To input the position settings from the points to the enemy object
        transform.position = patrolPoints[0].position;
        //To input the rotation settings from the points to the enemy object
        transform.rotation = patrolPoints[0].rotation;
        //Time to wait until the patrol starts
        waitTime = startWaitTime;
        //Setting the animator component to the enemy object
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
       //To make the patrol enemy walk from one start position to and end position, with some speed 
       transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

       //To choice whats happen when the enemy position is equal a point position
       if(transform.position == patrolPoints[currentPointIndex].position)
        {
            //To make the patrol enemy rotate the face
            transform.rotation = patrolPoints[currentPointIndex].rotation;
            //To the enemy animation
            anim.SetBool("isRunning", false);
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
            
        }else{
            anim.SetBool("isRunning", true);
        }
    }

    //Function to detect when the player hits the enemy and apply damage
    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.tag == "Player"){
            collison.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
