using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    // Start is called before the first frame update
    public GameObject fireball;
    public float timeBetweenShots;
    float nextShotTime;
    public Transform shotPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Make the Wizard enemy spawn projectils with a time between the shots
        if(Time.time > nextShotTime){
            Instantiate(fireball, shotPoint.position, transform.rotation);
            nextShotTime = Time.time + timeBetweenShots;
        }
    }
}
