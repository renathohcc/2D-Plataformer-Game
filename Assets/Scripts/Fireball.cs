using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int damage;
    public float lifeTime;
    void Start()
    {
        //Destroy the projectil if it don't hit any object after some time
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Make the projectil move
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    //Function to apply damage and destroy the projectil when it touch the Player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            collision.GetComponent<Player>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
