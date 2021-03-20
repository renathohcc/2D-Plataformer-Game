using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function to detect when the player hits the enemy and apply damage
    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.tag == "Player"){
            collison.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
